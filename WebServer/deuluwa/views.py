from rest_framework import viewsets
from deuluwa.serializers import UserSerializer
from deuluwa.models import User, Userinformation
from django.http import HttpResponse
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

    except:
        message='failed'

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
    except:
        message = 'failed'

    return HttpResponse(message)