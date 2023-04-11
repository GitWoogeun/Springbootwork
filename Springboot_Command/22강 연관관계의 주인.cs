
# 연관관계의 주인 = FK를 가진 오브젝트

메인페이지

1. 게시글 클릭 => 상세보기 화면 넘어감 
{                                                                         
    작성자 : rnb                          => username = [ User 테이블 ]     
    제목   : 오늘은 연관관계 주인 배우는 날 => title    = [ Board 테이블 ]    
    내용   : 어렵다                       => content  = [ Board 테이블 ]    
    댓글                                 => content  = [ Reply 테이블 ]
        ssar : 맞아요!                                                    
        cos  : 저도 배우고 있어요!                                          
}                                                                        
  [ MyBatis라면 ]               [ ORM 방식이라면 ]                                         
   3개를 Join해서                 Board 테이블만
   Select해서                     Select * From Board
   컬럼을 가져왔을 것              where Id = 1;
    
   자바프로그램  =>  JPA  =>  DB
                    Board모델의 ID가 1번이 필요하구나 
                    Board 테이블은 User라는 오브젝트를 가지고 있으니까
                    User테이블이랑 Join을 해서 가져와야한다고 JPA가 인식
                    JPA가 Join문을 DB에 날린다.
                    Join문은 user join board을 DB에 날린다.
                    그렇게되면 Board 오브젝트 안에 User 오브젝트 정보가 포함
                    Board를 Select하면 User도 같이 해준다. 

# JPA의 오브젝트 속성 자동 조인
Board 클래스 안에 User클래스의 변수가 존재(참조)하고 
Reply 클래스의 변수가 존재(참조)하고 있다면
JPA가 Join문을 3개에 테이블의 정보를 DB에 날린다.

하나의 게시글에는 한명의 유저가 담고 있고, 하나의 게시글은 한개가 될수있고, 몇천개가 될수있다.
Board에서 Join을해서 User는 한개면 되지만 댓글은 한개이면 안되기 때문에
List Collection이 들어가야한다.

ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

// Board라는 테이블에 userId 컬럼은 User테이블의  Id라는 컬럼을 참조하고 있다고 자동으로 FK 제약조건 생성
// Many = Board / One = User : 한명의 유저는 여러개의 게시글을 쓸수 있다.
// 데이터베이스의 컬럼으로는 userId로 만들어질거에요~
// DB는 오브젝트를 저장할 수 없다. FK, 자바는 오브젝트를 저장할 수 있다.
// 자바에서는 Object를 저장할 수 있다, 데이터베이스에는 오브젝트를 저장할 수 없다.
// 그렇기 때문에 자바가 데이터베이스에 맞춰서 JPA를 사용하면 오브젝트를 저장할 수 있다. ( FK가 된다 )
@ManyToOne (fetch = FetchType.EAGER)  	
@JoinColumn(name = "userId")    		
private User user; 				        
                                        
# @ManyToOne (fetch = FetchType.EAGER)
=> 한마디로 너가 board 테이블을 select하면 user테이블은 Join해서 가져올게 한건 밖에 없으니까.

# @OneToMany (fetch = FetchType.LAZY)
=> 필요하면 들고오고 필요하지 않으면 안들고 올게

//@JoinColumn(name = "replyId")	=> 이건 필요가 없다 : 실제 Board테이블에 Database에 FK replayId가 필요가 없다
//: 1 정규화가 깨짐 ( 하나의 컬럼은 원자성을 가진다 하나의 값을 가져야 한다. )
//하나의 게시글은 여러개의 댓글을 가질수 있기 때문에 OneToMany
@OneToMany(mappedBy = "board", fetch = FetchType.EAGER)		// mapped By 연관관계의 주인이 아니다 ( 난 FK가 아니에요~ ) DB에 컬럼을 만들지 마세요!
private List<Reply> replay;						// mapped By의 board는 Reply 클래스에 있는 Board 타입의 board 변수
// 나는 그냥 Board를 Select할때 Join문을 통해서 값을 얻기위해 필요한 겁니다.


2. 게시글

3. 게시글

이전페이지버튼 , 다음페이지버튼