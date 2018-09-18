from deuluwa.models import User, Userinformation
from rest_framework import serializers

class UserSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = User
        fields = ('id', 'password', 'admin')


class UserInformationSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Userinformation
        fields = ('id', 'address', 'phonenumber', 'name')