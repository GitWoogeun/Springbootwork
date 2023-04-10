
# 회원가입을 위한 insert 테스트

@ManyToMany : 두개의 테이블의 생성 시 중간 테이블을 만들어주는것 
=> ManyToMany는 사용하지 않는다. 그 이유는 서로의 primary key로만 중간 테이블을 생성해주는데
   날짜나 시간 다른 필드들이 필요할 수 있기 때문에 내가 직접만들고,
   @OneToMany, @OneToMany를 사용한다.;

# 회원가입은 Insert 이기 때문에 @PostMapping 어노테이션을 사용해야 한다.



# 회원 가입 시 유저를 구분하기위해 enum 사용 방법 :
// Enum은 어떤 데이터의 도메인 (범위)를 만들때 사용을 많이 한다.
public enum RoleType {
		USER, ADMIN  // ( 유저 , 관리자 )
}

# 회원가입을 테스트 하기 위한 DummyController
@RestController
public class DummyControllerTest {
		
    // DummyController가 메모리에 뜰떄 UserRepository 또한 같이 메모리에 뜬다. ( Spring이 Componunt할 떄 )
    @Autowired			// 의존성 주입 ( DI )
    private UserRepository userRepository;

    // @RequestParam("실제 DB의 컬럼명") String username 이렇게 @RequestParam 어노테이션을 사용한다면,
    // String username을 꼭 db의 컬럼명과 동일하게 적지 않아도 된다.
    
    // http://localhost:8000/blog/dummy/join (요청)
    // http의 body에 username, password, email 데이터를 가지고 (요청)하면
    // 또 다른 방법으로는 변수명을 실제 DB의 컬럼명과 동일하게 적는다면 @RequestParam 어노테이션을 사용하지 않아도 된다.
    @PostMapping("/dummy/join")		// => insert를 할꺼기 때문에 postMapping 사용
    public String join( User user ) {		// ( 이렇게 하면 오브젝트 형태로 받을 수 있다. )key = value 형태를 받아준다. ( 약속된 규칙 )
        System.out.println("userId     : " + user.getId());							// Id는 auto-increament가 된다 ( 시퀀스 )
        System.out.println("username   : " + user.getUsername());
        System.out.println("password   : " + user.getPassword());
        System.out.println("email      : " + user.getEmail());
        System.out.println("role       : " + user.getRole());							// default값이 작동을 하려면 insert할 때 role컬럼을 빼서 insert 한다.
        System.out.println("createDate : " + user.getCreateDate());			// 자바에서 @CreationTimestamp로 현재시간을 구해서 데이터를 넣어줌
        
        // Enum을 사용하여 실수를 방지할수 있다. ( 내가 넣는 값을 강제 할 수 있습니다. USER 아니면 ADMIN만 넣을수 있다 )
        // RoleType USER라고 명시
        user.setRole(RoleType.USER);			

        // 회원가입의 정보를 DB에 넣어주는 SAVE Function 실행!
        userRepository.save(user);
        
        return "회원가입 성공!";
    }
}

# ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
# @ColumnDefault와 @DynamicInsert 방식 사용
#    
//		// (" 'user' ") 문자열이라는걸 알려줘야한다.
//		@ColumnDefault("'user'")	// Enum을 사용하면 도메인을 설정할수있다. ( 도메인이 정해졌다 == 어떤 범위가 정해 졌다.  EX 성별이라치면 남, 녀로만 )
//		private String role;		// Enum을 쓰는게 좋다. ( Role : 어떤 회원가입을 했을 때 이 사람은 admin, user, manage )

# Enum방식으로 사용                                    
# DB는 RoleType 이라는게 없다. 그렇기 때문에 @Enumerated ( EnumType.STRING )으로 문자열이라고 명시 해줘야한다.
@Enumerated(EnumType.STRING)
private RoleType role; 	// Enum 방식 사용	// ADMIN, USER
