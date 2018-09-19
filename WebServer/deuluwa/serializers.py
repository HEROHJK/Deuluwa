from deuluwa.models import User, Userinformation, Courseinformation
from rest_framework import serializers

class UserSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = User
        fields = ('id', 'password', 'admin')


class UserInformationSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Userinformation
        fields = ('id', 'address', 'phonenumber', 'name')

class CourseInformationSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Courseinformation
        fields = ('coursename', 'classday', 'startdate', 'enddate', 'starttime', 'coursetime')