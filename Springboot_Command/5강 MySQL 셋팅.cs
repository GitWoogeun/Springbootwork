# MySQL 셋팅 :
=> 관리자 계정 : root
=> 관리자 비번 : cos1234

# 데이터베이스 생성
사용자 생성 및 권한 주기 및 DB 생성
# 유저 생성 ( 유저 계정 : cos , 유저 비번 : cos1234 )
create user 'cos'@'%' identified by 'cos1234';

# 유저 권한 부여 ( cos 유저에게 모든 권한 부여 )
GRANT ALL PRIVILEGES ON *.* TO 'cos'@'%';

# 데이터 베이스 생성 ( blog 데이터베이스 생성, CharacterSet과 컬렉터 UTF-8로 설정 )
CREATE DATABASE blog CHARACTER SET utf8 DEFAULT COLLATE utf8_general_ci;

# blog 데이터베이스 사용
use blog;


# MySQL 환경 설정 경로 : 
C:\ProgramData\MySQL\MySQL Server 5.7
=> my.ini 파일 편집
=> 아래 항목 추가
[client]
default-character-set=utf8

[mysql]
default-character-set=utf8

[mysqld]
collation-server = utf8_unicode_ci
init-connect='SET NAMES utf8'
init_connect='SET collation_connection = utf8_general_ci'
character-set-server=utf8


# 한글이 정상적으로 적용이 되었는지 확인 : 
show variables like 'c%';