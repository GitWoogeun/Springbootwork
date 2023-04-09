
# user 테이블의 character set latin으로 잡혀있을걸 UTF8로 변경

# MySQL : ROOT로 접속

# 쿼리 :
    // # 데이터베이스 blog를 새로 날린다.
    drop database blog;     
    // # 데이터베이스를 인코딩작업을 하고 blog 데이터베이스를 다시 만든다.
    CREATE DATABASE blog CHARACTER SET utf8 DEFAULT COLLATE utf8_general_ci;

# 새로 blog 데이터베이스에 접속 후
# JPA방식으로 user 테이블을 새로 생성 후 Character Set UTF8인지 확인하기