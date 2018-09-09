# 덤프파일 정보


## 사용 한 덤프 쿼리
`sudo -u postgres pg_dump deuluwa > filename.dump`

## 사용 방법
1. PostgreSQL 설치 후, `deuluwa` 라는 데이터 베이스를 생성.
2. 그 후 사용자를 만들어, DB에 모든 권한을 부여
3. 덤프 파일을 다운받아서 Owner : herohjk 부분을 사용자명으로 변경
4. `sudo -u postgres pg_dump deuluwa < filename.dump` 명령을 수행

## 현재까지의 ERD 구성 및 설명
### 사용자(user)
|물리 이름|논리 이름|비고|
|--|--|--|
|id|사용자id|text, not null, primary key|
|password|비밀번호|text,MD5로 암호화, not null|
|admin|관리자 여부|boolean, not null|

### 강의실(lectureRoom)
|물리이름|논리이름|비고|
|--|--|--|
|index|강의실 고유 id|int, not null, primary key, auto increment|
|nfcId|NFC 고유값|text, not null|
|latitude|강의실 위도|double, not null|
|longitude|강의실 경도|double, not null|

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
|index|수업 인덱스|int, not null, primary key, 수업 외래 키|
|startDate|시작일|date, not null|
|endDate|종료일|date, not null|
|startTime|수업 시작시간|char[4] (HH:MM), not null|
|courseTime|수업 진행 시간(분)|int, not null|
|classDay|수업 요일|char[7] (참,거짓), not null|

### 출석 기록(attendanceRecord)
|물리이름|논리이름|비고|
|--|--|--|
|userId|사용자 ID|text, not null, primary key, 사용자 외래 키|
|courseId|수업인덱스|int, not null, 수업 외래 키|
|attendanced|출석 여부|boolean, not null|
|attendanceTime| 출석 시간| date|
