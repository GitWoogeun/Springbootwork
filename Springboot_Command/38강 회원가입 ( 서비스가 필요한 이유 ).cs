
# 회원가입 ( 서비스가 필요한 이유 );

서비스 필요한 이유

1. 트랜잭션을 관리하기 위해서   -> 다음시간에 설명

2. 서비스 의미 때문

    송금 서비스 
    {
        홍길동 ========================> 송금 ========================> 임꺽정
         (5만)                         ( 3만 )                         ( 2만 )
    홍길동의 금액 업데이트                                           임꺽정 금액 업데이트
      2만원으로 업뎃                                                  5만원으로 업뎃
    }

repository는 C R U D를 들고 있다고 치면

송금 서비스는 U를 두개를 들고 있다가 홍길동 금액을 업데이트 치고, 임꺽정 금액을 업데이트 해서
오류가 없으면 커밋 
근데 둘중 하나가 실패를 했을 시 홍길동금액은 2만원으로 변했는데 임꺽정이 2만원 그대로면
둘다 ROLLBACK을 해야한다 원복 시켜야 한다.

서비스에서는 일 하나하나를 처리하는게 트랜잭션이라고 하는데
서비스는 이 두개의 트랜잭션을 하나로 합쳐서 하나의 트랜잭션으로 할 수 있다.;

#┌──────────────────────────────────────────────────────────
#│ UserService 영역 ( 실제 트랜잭션이 일어나는 곳 )
#└──────────────────────────────────────────────────────────
// 스프링이 컴포넌트 스캔을 통해서 빈에 등록을 해준다.  ( IoC를 해준다 ) 메모리를 대신 띄어준다.
@Service
public class UserService {
	
    @Autowired
    private UserRepository userRepository;
    
    // javax의 @Transaction
    // 여러개의 트랜잭션이 모여서 하나의 트랜잭션이 될수 있다.
    // 회원가입 함수의 전체가 하나의 트랜잭션이 된다. 전체가 실행이되면 그때 커밋이 될꺼구요
    // 트랜잭션중 하나라도 실패를 한다면 커밋이 되지 않고 RollBack이 될겁니다.
    @Transactional
    public int 회원가입(User user) {
        try {
            // userRepository 하나의 트랜잭션
            userRepository.save(user);
            return 1;
        } catch (Exception  e) {
            // TODO: handle exception
            e.printStackTrace();
            System.out.println("UserService : 회원가입() : " + e.getMessage());
        }
        return -1;
    }
}
#┌──────────────────────────────────────────────────────────
#│ UserApiController Service와 user.js 파일 연결  
#└──────────────────────────────────────────────────────────
// 얘는 나중에 웹에도 쓸수 있다.
//데이터만 리턴을 해주는 컨트롤러 이기 때문에 @RestController
@RestController						
public class UserApiController {

    // DI ( Spring이 컴포넌트 스캔할 때 스프링 빈을 통해서 IoC에 띄어준다 )
    @Autowired
    private UserService userService;

    // JSON이니까 @RequestBody로 파라미터 받음
    // 통신상태를 확인하기 위해 HttpStatus.OK 
    @PostMapping("/api/user")
    public ResponseDto<Integer> save(@RequestBody User user) {		
        System.out.println("UserApiController : save 호출됨!");
        
        // username, password, email 입력값 form태그에서 받음
        // role은 back단에서 직접 넣어줘야함
        user.setRole(RoleType.USER);
        
        // 실제로 여기서 DB에 insert를 하고 아래에서 return이 되면 되요.
        int result = userService.회원가입(user);
        
        // 회원가입이 성공 시 [ 상태값 : 200, 1 ] 호출
        // 자바 오브젝트를 JSON으로 변환해서 리턴 (Jackson)
        return new ResponseDto<Integer>(HttpStatus.OK, result);	
    }
}