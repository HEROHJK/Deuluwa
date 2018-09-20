from rest_framework import viewsets
from deuluwa.serializers import UserSerializer
from deuluwa.models import User, Userinformation, Courseinformation
from django.http import HttpResponse
from deuluwa.funcs import getEndTime
from datetime import datetime
import json

class UserViewSet(viewsets.ModelViewSet):
    serializer_class = UserSerializer
    queryset = User.objects.all()

def getUserInfo(request):
    message = 'failed'
    try :
        inputId = request.GET.get('id')
        inputPw = request.GET.get('password')

        command = "SELECT * FROM deuluwa.public.user WHERE id = '{id}' AND password = MD5('{password}');".format(id=inputId,password=inputPw)

        if len(list(User.objects.raw(command))) > 0:
            message = 'success'

    except Exception as e:
        print("실패 원인 : " + str(e))
        message = 'failed'

    return HttpResponse(message)

def getUserAddInfo(request):
    message = 'failed'
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
    message = 'failed'
    try :
        inputId = request.GET.get('id')
        command = "SELECT * FROM courseinformation, coursestudent WHERE courseinformation.courseindex = coursestudent.couseid AND userid = '{id}';".format(id=inputId)

        userClasses = Courseinformation.objects.raw(command)

        userClassesList = []

        for userClass in userClasses:
            learningTime = getEndTime(userClass.starttime, userClass.coursetime)
            userClassesList.append(
                {'index':userClass.index,
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