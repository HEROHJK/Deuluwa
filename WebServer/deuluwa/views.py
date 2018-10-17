from deuluwa.models import User, Userinformation, Courseinformation, Attendancerecord, Notice
from django.http import HttpResponse
from deuluwa.funcs import getEndTime, getTime, tardyCheck, makeDateTime
from django.db import connection
from django.views.decorators.csrf import csrf_exempt
import json
import datetime

#사용자 로그인
def getUserInfo(request):
    try :
        inputId = request.GET.get('id')
        inputPw = request.GET.get('password')

        command = "SELECT * FROM deuluwa.public.user WHERE id = '{id}' AND password = MD5('{password}');".format(id=inputId,password=inputPw)
        list = User.objects.raw(command)
        if len(list) > 0:
            if(list[0].admin == True):
                message = 'success admin'
            else:
                message = 'success'
        else:
            message = 'failed'

    except Exception as e:
        print("실패 원인 : " + str(e))
        message = 'failed'

    return HttpResponse(message)

#사용자 상세정보 조회
def getUserAddInfo(request):
    try :
        inputId = request.GET.get('id')

        userInfo = Userinformation.objects.filter(id__userinformation=inputId).first()

        address = userInfo.address
        phone = userInfo.phonenumber
        name = userInfo.name

        jsonData = {"address":address, "phonenumber":phone, "name":name}
        message = json.dumps(jsonData,ensure_ascii=False)

    except Exception as e:
        print("실패 원인 : " + str(e))
        message = 'failed'

    return HttpResponse(message)

#수강생 수강목록 조회
def getUserCourseList(request):
    try :
        inputId = request.GET.get('id')
        command = "SELECT * FROM courseinformation, coursestudent WHERE courseinformation.courseindex = coursestudent.couseid AND userid = '{id}';".format(id=inputId)

        userClasses = Courseinformation.objects.raw(command)
        userClassesList = []

        for userClass in userClasses:
            learningTime = getEndTime(userClass.starttime, userClass.coursetime)
            userClassesList.append(
                {'index':userClass.courseindex.index,
                 'coursename' : userClass.coursename,
                 'classday' : userClass.classday,
                 'startdate' : userClass.startdate.strftime("%Y-%m-%d"),
                 'enddate' : userClass.enddate.strftime("%Y-%m-%d"),
                 'starttime' : learningTime[0].strftime("%H:%M"),
                 'endtime' : learningTime[1].strftime("%H:%M")
                 }
            )
        message = json.dumps(userClassesList,ensure_ascii=False)

    except Exception as e:
        print("실패 원인 : " + str(e))
        message = 'failed'

    return HttpResponse(message)

#출석정보 조회
def getAttendanceCheckList(request):
    try:
        inputCourseId = request.GET.get('courseid')
        inputId = request.GET.get('id')

        objects = Attendancerecord.objects.filter(userid=inputId).filter(courseid=inputCourseId).order_by('-checkdate')[:5]

        courseTime = getEndTime(Courseinformation.objects.filter(courseindex=inputCourseId)[0].starttime,
                                   Courseinformation.objects.filter(courseindex=inputCourseId)[0].coursetime)

        attendanceList = []

        for result in objects:

            checkTime = getTime(result.checktime.strip())
            attendanceList.append({
                'checkdate' : str(result.checkdate),
                'checktime' : str(result.checktime),
                'attendance' : str(tardyCheck(courseTime[0], courseTime[1], checkTime))
            })

        message = json.dumps(attendanceList, ensure_ascii=False)
    except Exception as e:
        message = 'failed : ' + str(e)

    return HttpResponse(message)

#수업 상세정보 출력
def getCourseTotalInformation(request):
    try:
        inputCourseId = request.GET.get('courseid')
        cursor = connection.cursor()
        command = "SELECT * FROM courseinfoview WHERE index='{index}';".format(index=inputCourseId)
        cursor.execute(command)
        result = cursor.fetchall()
        if(len(result) > 0):
            checkTime = getEndTime(result[0][3],result[0][4])

            myTuple = {'index':str(result[0][0]),
                       'coursename':str(result[0][1]),
                       'teacher':str(result[0][2]),
                       'starttime':str(checkTime[0].strftime("%H:%M")),
                       'endtime':str(checkTime[1].strftime("%H:%M")),
                       'roomname':str(result[0][5]),
                       'classday':str(result[0][6]),
                       'startdate':str(Courseinformation.objects.filter(courseindex=result[0][0]).first().startdate),
                       'enddate': str(Courseinformation.objects.filter(courseindex=result[0][0]).first().enddate)
                       }
            message = json.dumps(myTuple,ensure_ascii=False)

        else:
            message='failed'

    except Exception as e:
        message = 'failed : ' + str(e)

    return HttpResponse(message)

#사용자 목록 출력
def getUserList(request):
    try:
        objects = Userinformation.objects.order_by('id').all()

        list = []

        message='test'
        for obj in objects:
            list.append({
                'id' : str(obj.id.id),
                'name' : str(obj.name),
                'address' : str(obj.address),
                'phonenumber' : str(obj.phonenumber),
                'admin' : str(obj.id.admin)
            })

        message = json.dumps(list, ensure_ascii=False)

    except Exception as e:
        message = 'failed : ' + str(e)
        print(message)

    return HttpResponse(message)

#공지사항 출력
def getNoticeMessages(request):
    noticeMessages = Notice.objects.order_by('-index')

    noticeList = []

    for result in noticeMessages:
        noticeList.append({
            'index' : result.index,
            'message' : result.message,
            'user' : Userinformation.objects.filter(id__userinformation=result.user.id).first().name,
            'time' : makeDateTime(str(result.date).strip(), str(result.time).strip())
        })

    message = json.dumps(noticeList, ensure_ascii=False)

    return HttpResponse(message)

#공지사항 입력
@csrf_exempt
def writeNoticeMessage(request):
    try:
        inputId = request.POST['id']
        inputMessage = request.POST['message']

        message = inputMessage

        time = datetime.datetime.now()

        query = Notice.objects.create(
            user=User.objects.filter(id=inputId).first(),
            message=inputMessage,
            date=time.strftime('%Y-%m-%d'),
            time=time.strftime('%H:%M:%S')
        )
        query.save()

        noticeMessages = Notice.objects.order_by('-index')

        noticeList = []

        for result in noticeMessages:
            noticeList.append({
                'index': result.index,
                'message': result.message,
                'user': Userinformation.objects.filter(id__userinformation=result.user.id).first().name,
                'time': makeDateTime(str(result.date).strip(), str(result.time).strip())
            })

        message = json.dumps(noticeList, ensure_ascii=False)

    except Exception as e:
        message = 'failed : ' + str(e)

    return HttpResponse(message)

#사용자 정보 업데이트
@csrf_exempt
def updateUserInformation(request):
    try:
        inputId = request.POST['id']
        inputName = request.POST['name']
        inputPhonenumber = request.POST['phonenumber']
        inputAddress = request.POST['address']

        userinfo = Userinformation.objects.get(id=inputId)
        print(userinfo)
        userinfo.name=inputName
        userinfo.phonenumber=inputPhonenumber
        userinfo.address=inputAddress
        userinfo.save()

        objects = Userinformation.objects.order_by('id').all()

        list = []

        message = 'test'
        for obj in objects:
            list.append({
                'id': str(obj.id.id),
                'name': str(obj.name),
                'address': str(obj.address),
                'phonenumber': str(obj.phonenumber),
                'admin': str(obj.id.admin)
            })

        message = json.dumps(list, ensure_ascii=False)
        
    except Exception as e:
        message = 'failed : ' + str(e)

    return HttpResponse(message)

#사용자 등록
@csrf_exempt
def addUser(request):
    try:
        inputId = request.POST['id']
        inputName = request.POST['name']
        inputPhonenumber = request.POST['phonenumber']
        inputAddress = request.POST['address']
        inputAdmin = request.POST['admin']

        admin = inputAdmin == 'True' if True else False

        cursor = connection.cursor()
        #command = "SELECT * FROM courseinfoview WHERE index='{index}';".format(index=inputCourseId)
        command = "SELECT MD5('{md5pass}');".format(md5pass = inputPhonenumber)
        cursor.execute(command)
        makePassword = cursor.fetchall()[0][0]

        User.objects.create(id=inputId, password=makePassword, admin=admin)

        Userinformation.objects.create(
            id=User.objects.filter(id=inputId).get(),
            name=inputName,
            phonenumber=inputPhonenumber,
            address=inputAddress,
        )

        objects = Userinformation.objects.order_by('id').all()

        list = []

        for obj in objects:
            list.append({
                'id': str(obj.id.id),
                'name': str(obj.name),
                'address': str(obj.address),
                'phonenumber': str(obj.phonenumber),
                'admin': str(obj.id.admin)
            })

        message = json.dumps(list, ensure_ascii=False)

    except Exception as e:
        message = 'failed : ' + str(e)

    return HttpResponse(message)

#비밀번호 초기화
@csrf_exempt
def passwordReset(request):
    try:
        inputId = request.GET.get('id')

        user = User.objects.filter(id=inputId).first()

        cursor = connection.cursor()
        # command = "SELECT * FROM courseinfoview WHERE index='{index}';".format(index=inputCourseId)
        command = "SELECT MD5('{md5pass}');".format(md5pass=Userinformation.objects.filter(id=user).first().phonenumber)
        cursor.execute(command)
        makePassword = cursor.fetchall()[0][0]

        user.password=makePassword
        user.save()

        message='success'


    except Exception as e:
        message = 'failed : ' + str(e)

    return HttpResponse(message)