# Deuluwa
출결관리 어플리케이션, 드루와!

![](https://raw.githubusercontent.com/HEROHJK/Deuluwa/master/DeuluwaMain.png)

## 프로젝트 소개 및 설명
C# 기반의 크로스 플랫폼 Xamarain.Forms을 이용해 개발하는 프로젝트로, GPS, NFC, QR코드를 이용한 출석 체크 어플리케이션을 개발한다.

사용자의 시퀀스는 다음과 같다.

1. 어플리케이션을 실행한다.
2. 어플리케이션에서 출석 메뉴를 들어간다
3. 출석 메뉴 화면에서 NFC를 인식할 수 있지만, QR코드 인식을 위해 카메라열기버튼을 준비시킨다.
4. 등록된 값으로 출석을 하는데, 위치와 시간을 확인한다.(DB 조회)
5. 위치가 다르면 출석이 되지 않으며, 시간이 다르면 출석이 되지만, 지각에 관한 처리를 한다.(DB 등록)

관리자 프로그램 또한 만들어야 하며, 우선은 C# Winform으로 개발을 하고, 추후 파이썬 dJango를 이용하여 웹으로 전환한다.

## 플랫폼 소개
### 플랫폼 별 사용 환경

본 프로그램이 지원하는 플랫폼은 다음과 같다.

* Android 7.1 Nougat 이상의 스마트폰
* iOS 스마트폰

관리자 프로그램이 지원하는 플랫폼은 다음과 같다

* Winform .Net 프레임워크 4.7
* (추후) python dJango 웹
### 개발 환경
개발 환경은 다음과 같다.
* Visual Studio 2017 Xamarin.Forms 솔루션
* Android SDK API Level 25
* iOS

## 구현 사항
구현해야할 사항은 다음과 같다.

* [DB 설계](https://github.com/HEROHJK/Deuluwa/tree/master/SQL)
* Xamarin을 이용한 안드로이드 iOS 프론트 디자인
* Xamarin을 이용한 안드로이드, iOS 위치정보, NFC태그, 카메라 QR코드 기능 개발
* Xamarin을 이용한 DB제어(추후 파이썬 서버로 수정 예정)
* Winform을 이용한 관리자 프로그램

## [프로토 타입](https://ovenapp.io/view/ZuTOW7cSXyGGQAZZ4GlItVUUocaTsaYJ/)
