from rest_framework import viewsets
from deuluwa.serializers import UserSerializer
from deuluwa.models import User, Userinformation, Courseinformation, Attendancerecord
from django.http import HttpResponse
from deuluwa.funcs import getEndTime, getTime, tardyCheck
from django.db import connection
import json

class UserViewSet(viewsets.ModelViewSet):
    serializer_class = UserSerializer
    queryset = User.objects.all()

def getUserInfo(request):
    try :
        inputId = request.GET.get('id')
        inputPw = request.GET.get('password')

        command = "SELECT * FROM deuluwa.public.user WHERE id = '{id}' AND password = MD5('{password}');".format(id=inputId,password=inputPw)

        if len(list(User.objects.raw(command))) > 0:
            message = 'success'
        else:
            message = 'failed'

    except Exception as e:
        print("실패 원인 : " + str(e))
        message = 'failed'

    return HttpResponse(message)

def adminLogin(request):
    try :
        inputId = request.GET.get('id')
        inputPw = request.GET.get('password')

        command = "SELECT * FROM deuluwa.public.user WHERE id = '{id}' AND password = MD5('{password}') AND admin = true;".format(
            id=inputId, password=inputPw)

        if len(list(User.objects.raw(command))) > 0:
            message = 'success'
        else:
            message = 'failed'

    except Exception as e:
        print("failed : " + str(e))
        message = "failed : " + str(e)

    return HttpResponse(message)

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

def getCourseInformation(request):
    try:
        inputCourseId = request.GET.get('courseid')
        cursor = connection.cursor()
        cursor.execute("SELECT courseinformation.coursename, userinformation.name teacher, courseinformation.starttime, courseinformation.coursetime, lectureroom.name lectureroomname FROM courseinformation, userinformation, lectureroom, course WHERE courseinformation.courseindex = course.index AND course.lectureroomindex = lectureroom.index AND course.lectureindex = userinformation.id AND course.index = '{index}';".format(index=inputCourseId))

        raw = cursor.fetchone()

        jsondata = {'coursename':str(raw[0]),
                    'teacher':str(raw[1]),
                    'starttime':str(raw[2]),
                    'coursetime':str(raw[3]),
                    'lectureroomname':str(raw[4])}

        message = json.dumps(jsondata,ensure_ascii=False)

    except Exception as e:
        print('실패 원인 : ' + str(e))
        message = 'failed'

    return HttpResponse(message)

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
                       'classday':str(result[0][6])}
            message = json.dumps(myTuple,ensure_ascii=False)

        else:
            message='failed'

    except Exception as e:
        message = 'failed : ' + str(e)

    return HttpResponse(message)