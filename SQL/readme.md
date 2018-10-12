
# SQL 정보

## 사양
* 하드웨어 - 라즈베리파이 2B
* PostgreSQL 9.6.10 on armv7l-unknown-linux-gnueabihf, compiled by gcc (Raspbian 6.3.0-18+rpi1) 6.3.0 20170516, 32-bit

## 왜 PostgreSQL인가?
* **처음 써보는 DB**
* 그중, 오픈소스이기 때문에 라이센스에 대한 부담이 없음
* 무료중에는, MySQL 다음으로 많은 사용자
* (사실 처음써보는거라 아는게 별로 없음)

## 사용 한 덤프 쿼리
`sudo -u postgres pg_dump deuluwa > filename.dump`

## 사용 방법
1. PostgreSQL 설치 후, `deuluwa` 라는 데이터 베이스를 생성.
2. 그 후 사용자를 만들어, DB에 모든 권한을 부여
3. 덤프 파일을 다운받아서 Owner : herohjk 부분을 사용자명으로 변경
4. `sudo -u postgres pg_dump deuluwa < filename.dump` 명령을 수행

## 현재까지의 ERD 구성 및 설명

### ERD
![enter image description here](https://raw.githubusercontent.com/HEROHJK/Deuluwa/master/SQL/deuluwadb.png)

### 사용자(user)
|물리 이름|논리 이름|비고|
|--|--|--|
|id|사용자id|text, not null, primary key|
|password|비밀번호|text,MD5로 암호화, not null|
|admin|관리자 여부|boolean, not null|

### 사용자 상세 정보(userInformation)
|물리 이름|논리 이름|비고|
|--|--|--|
|id|사용자 id|text, primary key, 사용자 외래 키|
|address|주소|text|
|phoneNumber|전화번호|text|
|name|이름|text|

### 강의실(lectureRoom)
|물리이름|논리이름|비고|
|--|--|--|
|index|강의실 고유 id|int, not null, primary key, auto increment|
|nfcId|NFC 고유값|text, not null|
|latitude|강의실 위도|double, not null|
|longitude|강의실 경도|double, not null|
|name|강의실 이름|text, not null|

### 수업(course)
|물리이름|논리이름|비고|
|--|--|--|
|index|인덱스|int, not null, primary key, auto increment|
|lectureIndex|강의자 ID|text, notnull, 사용자의 외래 키|
|lectureRoomIndex|강의실 인덱스|int, not null, 강의실 외래 키|
|informationIndex|강의 상세 정보|int, not null|

### 수업 상세(courseInformation)
|물리이름|논리이름|비고|
|--|--|--|
|index|인덱스|int, primary key|
|courseindex|수업 인덱스|int, not null, 수업 외래 키|
|startDate|시작일|date, not null|
|endDate|종료일|date, not null|
|startTime|수업 시작시간|char[4] (HH:MM), not null|
|courseTime|수업 진행 시간(분)|int, not null|
|classDay|수업 요일|char[7] (참,거짓), not null|
|coursename|수업명|text, not null|

### 수강생(courseStudent)
|물리이름|논리이름|비고|
|--|--|--|
|courseId|수업인덱스|int, not null, primary key, 수업 외래키|
|userId|사용자 ID|text, not null, primary key, 사용자 외래키|

### 출석 기록(attendanceRecord)
|물리이름|논리이름|비고|
|--|--|--|
|userId|사용자 ID|text, not null, primary key, 사용자 외래 키|
|courseId|수업인덱스|int, not null, 수업 외래 키|
|attendanced|출석 여부|boolean, not null|
|checkdate| 출석 일자| date, not null|
|checktime| 출석 시간| time, not null|

### 로그인세션(loginSession)
|물리이름|논리이름|비고|
|--|--|--|
|userId|사용자 ID|text, not null, primnary key,  사용자 외래 키|
|value|세션값|text, not null|
|lastTime|마지막 응답 시간|date, not null|

### 전달사항(notice)
|물리이름|논리이름|비고|
|--|--|--|
|index|번호|int, not null, primarykey|
|message|글 내용|text, not null|
|user|글쓴이|text, not null, 사용자 외래키|
|date|날짜|date, not null|
|time|시간|time, not null|

### 사용자뷰(userview)
|물리이름|논리이름|비고|
|--|--|--|
|id|id|user table|
|admin|관리자여부|user table|
|name|이름|userinformation table|
|address|주소|userinformation table|
|phonenumber|연락처|userinformation table|

### 수업정보뷰(courseinfoview)
|물리이름|논리이름|비고|
|--|--|--|
|index|번호|course table|
|coursename|수업명|courseinformation table|
|teacher|강사명|course -> user -> userinformation table|
|starttime|시작시간|coursetinformation table|
|coursetime|진행시간|courseinformation table|
|roomname|강의실명|course -> lectureroom table|
|classday|진행요일|courseinformation table|