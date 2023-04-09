
# 1. Blog 테이블 만들기
# 2. 연관관계 만들기
# 3. 더미 데이터 insert

# 1. Blog 테이블 생성하기
jpa:
    open-in-view: true
    hibernate:
      ddl-auto: create                        // create :  프로젝트가 재실행 할때 마다 테이블을 새로 만들겠다. ( 최초시에만 create로 )
      naming:                                 // update : 프로젝트가 재실행 할때 마다 테이블을 업데이트 하겠다.
        physical-strategy: org.hibernate.boot.model.naming.PhysicalNamingStrategyStandardImpl
      use-new-id-generator-mappings: false    // MySQL일경우 auto_increament 어떤식으로 사용할지 방식을 결정 : false일 경우 JPA가 사용하는 기본 넘버링 전략을 따라가지 않는다.
    show-sql: true                            // 프로젝트에서 연결된 DB의 넘버링 전략을 따라간다. ( 참고 User 클래스 )
    properties:
      hibernate.format_sql: true
    # 프로젝트 실행 시 제대로 테이블이 생성 되었다면
    {   // jpa:
        //   hibernate:
        //      ddl-auto: create            => 프로젝트가 재실행 될때 마다 기존 테이블 삭제 후 테이블 새로 생성 )
        //      naming:
        //          physical-strategy:      => 엔티티를 만들 때 (테이블을 만들 떄) 변수명 그대로 데이터베이스 필드에 넣어준다.
        //   show-sql: true                 => 콘솔창에 테이블이 생성되는지 쿼리를 보여줌
        //   properties:
        //      hibernate.format_sql: true  => 콘솔에 쿼리를 예쁘게 정렬해서 보여준다.
        drop table if exists User
    Hibernate: 
        
        drop table if exists User
    [2m2023-04-09 17:51:06.516[0;39m [32mDEBUG[0;39m [35m15964[0;39m [2m---[0;39m [2m[  restartedMain][0;39m [36morg.hibernate.SQL                       [0;39m [2m:[0;39m 
        
        create table User (
        id integer not null auto_increment,
            createDate datetime(6),
            email varchar(50) not null,
            password varchar(100) not null,
            role varchar(255) default 'user',
            username varchar(30) not null,
            primary key (id)
        ) engine=InnoDB
    Hibernate: 
        
        create table User (
        id integer not null auto_increment,
            createDate datetime(6),
            email varchar(50) not null,
            password varchar(100) not null,
            role varchar(255) default 'user',
            username varchar(30) not null,
            primary key (id)
        ) engine=InnoDB
    }