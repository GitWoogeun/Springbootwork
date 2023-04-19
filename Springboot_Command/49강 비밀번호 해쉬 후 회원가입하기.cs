┌─────────────────────────────────────────────────────────────────────────────────
│ 비밀번호 해쉬 후 회원 가입하기 
└─────────────────────────────────────────────────────────────────────────────────

해쉬란?

안녕 => 해쉬 암호화 => ABC365FC ( 고정 길이의 16진수 값으로 변경 됨 )
안녕 => 해쉬 암호화 => ABC365FC ( 여러번 요청을 해도 같은 값으로 변경 됨 )
안녕! => 해쉬 암호화 => 3671AF28 ( ! 하나를 추가해도 값이 전체가 변경된다. )


# 데이터가 변경이 되었는지 확인 하는 방법
처음으로 요청한 해쉬로 변경된 값과
두번째로 요청한 해쉬로 변경된 값과 다르다면 데이터가 변경이 있다는걸 확인할 수 있다.

@Service
public class UserService {
	
    @Autowired
    private UserRepository userRepository;					    // userRepository  
    
    @Autowired
    private BCryptPasswordEncoder encoder;				        // Password 암호화를 하기 위한 객체 생성
    
    // javax의 @Transaction
    // 회원가입 함수의 전체가 하나의 트랜잭션이 된다. 전체가 실행이되면 그때 커밋이 될꺼구요
    // 트랜잭션중 하나라도 실패를 한다면 커밋이 되지 않고 RollBack이 될겁니다.
    @Transactional
    public void 회원가입(User user) {
        
        String rawPassword = user.getPassword();   				// 원문 비밀번호
        String encPassword = encoder.encode(rawPassword);		// 암호화 비밀번호 ( 해쉬 처리가 됨 )
        
        user.setPassword(encPassword);							// 비밀번호 암호화 적용된 데이터 Password에 셋팅
        
        userRepository.save(user);								// userRepository 하나의 트랜잭션
    }
}