"""WebServer URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/2.1/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.contrib import admin
from django.urls import path
from django.conf.urls import url, include
from deuluwa import views

urlpatterns = [
    #관리자 페이지(미제작)
    path('admin/', admin.site.urls),
    #사용자 로그인
    url(r'^user/$', views.getUserInfo),
    #사용자 추가 정보 조회
    url(r'^userinfo/$', views.getUserAddInfo),
    #사용자 수강목록 조회
    url(r'^usercourselist/$', views.getUserCourseList),
    #수업 정보 조회
    url(r'^courseinformation/$',views.getCourseInformation),
    #수업 사용자 출석목록 조회
    url(r'^userattendancelist/$', views.getAttendanceCheckList),
    #수업 상세정보 조회
    url(r'^coursetotalinformation/$', views.getCourseTotalInformation),
    #공지사항 조회
    url(r'^notice', views.getNoticeMessages),
    #공지사항 작성
    url(r'^writenoticemessage', views.writeNoticeMessage),
    #사용자목록 조회
    url(r'^userlist', views.getUserList),
]
