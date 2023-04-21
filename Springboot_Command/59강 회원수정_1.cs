#┌─────────────────────────────────────────────────────────────────────────────────
#│ 회원 수정
#└─────────────────────────────────────────────────────────────────────────────────


#┌─────────────────────────────────────────────────────────────────────────────────
#│ updateForm.jsp ( 유저 )
#└─────────────────────────────────────────────────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ include file="../layout/header.jsp"%>
<div class="container">
	<form>
		<input type="hidden" id="id" value="${principal.user.id}"/>
		<div class="form-group">
			<label for="username">UserName :</label> 
            // principal : 인증된 사용자의 user의 username을 가져온다.
			<input type="text" value="${principal.user.username}" placeholder="Enter username" class="form-control"  id="username" readonly="readonly">
		</div>
		<div class="form-group">
			<label for="password">User Password :</label> 
			<input type="password"  class="form-control" placeholder="비밀번호를 수정해주세요." id="password">
		</div>
		<div class="form-group">
			<label for="email">User Email :</label>
			 <input type="email" value="${principal.user.email}" class="form-control" placeholder="Enter email" id="email">
		</div>
	</form>
    // jquery 방식의 form태그 데이터 전송 
	<button id="btn-update" class="btn btn-primary">수정하기</button>
</div>
// user의 JavaScript를 가져온다.
<script src="/js/user.js"></script>
<%@ include file="../layout/footer.jsp"%>

#┌─────────────────────────────────────────────────────────────────────────────────
#│ userController ( 회원정보 수정 화면으로 이동하는 Controller [GET 방식])
#└─────────────────────────────────────────────────────────────────────────────────
// 인증이 안된 사용자들이 출입할 수 있는 경로를 /auth/** 경로 허용
// 그냥 주소가 / 이면 index.jsp 허용
// static 이하에 있는 /js/**. /css/**. /image/** 허용
@Controller
public class UserController {
	
	// 회원가입
	@GetMapping("/auth/joinForm")
	public String joinForm() {
		return "user/joinForm";
	}
	
	// 로그인 
	@GetMapping("/auth/loginForm")
	public String loginForm() {
		return "user/loginForm";
	}
	
	// 회원수정
	@GetMapping("/user/updateForm")
	public String updateForm() {
		return "user/updateForm";
	}
}

#┌─────────────────────────────────────────────────────────────────────────────────
#│ userApiController (실제 데이터를 주고 받는 Controller) user.js 호출
#└─────────────────────────────────────────────────────────────────────────────────
// 얘는 나중에 웹에도 쓸수 있다.
//데이터만 리턴을 해주는 컨트롤러 이기 때문에 @RestController
@RestController						
public class UserApiController {

    // DI ( Spring이 컴포넌트 스캔할 때 스프링 빈을 통해서 IoC에 띄어준다 )
    @Autowired
    private UserService userService;

    // JSON이니까 @RequestBody로 파라미터 받음
    // 통신상태를 확인하기 위해 HttpStatus.OK
    // username, password, email 
    @PostMapping("/auth/joinProc")
    public ResponseDto<Integer> save(@RequestBody User user) {			

        System.out.println("UserApiController : save 호출됨!"); 
        
        // 실제로 여기서 DB에 insert를 하고 아래에서 return이 되면 되요.
        userService.회원가입(user);
        
        // 회원가입이 성공 시 [ 상태값 : 200, 1 ] 호출
        // 자바 오브젝트를 JSON으로 변환해서 리턴 (Jackson)
        return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);			
    }

    // 유저의 정상적인 통신이 된다면 상태값 200(성공)과 1을 리턴   
    @PutMapping("/user")
    public ResponseDto<Integer> update(@RequestBody User user){		// key = value, x-www-form-urlencoded
        userService.회원수정(user);
        return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);
    }
}

#┌─────────────────────────────────────────────────────────────────────────────────
#│ userService ()
#└─────────────────────────────────────────────────────────────────────────────────
// 스프링이 컴포넌트 스캔을 통해서 빈에 등록을 해준다.  ( IoC를 해준다 ) 메모리를 대신 띄어준다.
@Service
public class UserService {
	
    // JPA방식의 데이터베이스 접근
    @Autowired
    private UserRepository userRepository;
    
    // Password 암호화를 하기 위한 객체 생성
    @Autowired
    private BCryptPasswordEncoder encoder;
    
    // javax의 @Transaction
    // 회원가입 함수의 전체가 하나의 트랜잭션이 된다. 전체가 실행이되면 그때 커밋이 될꺼구요
    // 트랜잭션중 하나라도 실패를 한다면 커밋이 되지 않고 RollBack이 될겁니다.
    @Transactional
    public void 회원가입(User user) {
        String rawPassword = user.getPassword();   			// 원문 비밀번호
        String encPassword = encoder.encode(rawPassword);	// 암호화 비밀번호 ( 해쉬 처리가 됨 )			
        user.setPassword(encPassword);						// 비밀번호 암호화 적용된 데이터 Password에 셋팅			
        userRepository.save(user);							// userRepository 하나의 트랜잭션
    }
    
    @Transactional
    public void 회원수정(User user) {
        
        // 여기서 찾는 Id는 principal.user.userid 값을 찾는 것 ( 위치 : 영속성컨텍스트 )
        // SELECT해서 ID를 찾는다면 영속성컨텍스트의 저장된 USER의 정보를 수정 후
        // 영속성컨텍스트 flush => 데이터베이스 저장 
        User persistence = userRepository.findById(user.getId())
                .orElseThrow(()-> {
                        return new IllegalArgumentException("회원 찾기 실패");
                });
        String rawPassword = user.getPassword();
        String encPassword = encoder.encode(rawPassword);
        
        // 회원 수정 함수 종료 시 = 서비스 종료 = 트랜잭션 종료 = 커밋이 자동으로 됩니다.
        // 영속화된 persistence 객체의 변화가 감지되면 더티체킹이 되어 update문을 날려줌.
        persistence.setPassword(encPassword);
        persistence.setEmail(user.getEmail());
    }
}

#┌─────────────────────────────────────────────────────────────────────────────────
#│ user.js
#└─────────────────────────────────────────────────────────────────────────────────
// 제이쿼리 사용
let index = {
    init: function() {
        // on : 첫번재  파라미터를 결정하고 클릭이 되면 두번째 파라미터가 무엇을할지 정하면 된다.
        // => : 화살표 함수 : function() {} / 대신 () => {} 사용한 이유는 this를 바인딩하기 위해서 사용
        $("#btn-save").on("click", ()=>{
                this.save();		// <= 회원가입 버튼을 클릭 했을 시 save: function()을 호출
        });
        $("#btn-update").on("click", ()=>{
                this.update();
        });
    },
    
    save: function(){
        // alert("user의 save함수 호출됨");
        let data = {
            username: $("#username").val(),		// form태그에 있는 태그의 id값을 찾아서 username 변수에 값을 바인딩 한다.
            password: $("#password").val(),		// form태그에 있는 태그의 id값을 찾아서 password  변수에 값을 바인딩 한다.
            email: $("#email").val()			// form태그에 있는 태그의 id값을 찾아서 email          변수에 값을 바인딩 한다.
        };
        // 데이터를 잘 가져오는지 확인
        // console.log(data);
        
        // ajax 통신을 이용해서 3개의 데이터를 JSON으로 변경하여 insert 요청!!!
        // ajax 호출 시 default가 비동기 호출
        // 회원가입 수행 요청 ( 100초 가정 ) 작업중이라고 해도 // fail 함수 밑에 프로세스를 실행함
        // ajax가 통신을 성공하고 나서 서버가 json을 리턴해주면 자동으로 자바 오브젝트로 변환해주네요 
        $.ajax({
            type: "post",						// POST방식으로 전송
            url: "/auth/joinProc",				// UserApiController의 save() 함수 호출					
            data: JSON.stringify(data),		    // JSON 문자열로 data ( http body 데이터 ) 변경 (MIME 타입이 필요)
            contentType: "application/json; charset=utf-8",
            dataType: "json"					// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
                                                // dataType : JSON => 생긴게 JSON이라면 => javascript로 오브젝트 변경 
        }).done(function(resp){					    
            // 응답의 결과가 정상이면 done을 실행되는 영역
            console.log(resp);
            alert("회원가입이 완료 되었습니다.");
            location.href = "/";				// 정상적으로 회원가입 후 다시 /blog url로 이동 
            
        }).fail(function(error){
            // 응답의 결과가 실패 하면 fail을 실행
            alert(JSON.spstringify(error));
        });
    },
    
    update: function(){
        // alert("user의 save함수 호출됨");
        let data = {
            id: $("#id").val(),
            password: $("#password").val(),	// form태그에 있는 태그의 id값을 찾아서 password  변수에 값을 바인딩 한다.
            email: $("#email").val()		// form태그에 있는 태그의 id값을 찾아서 email          변수에 값을 바인딩 한다.
        };
        $.ajax({
            type: "put",					// PUT 방식의 전송
            url: "/user",					// UserApiController의 update()함수 호출					
            data: JSON.stringify(data),		// JSON 문자열로 data ( http body 데이터 ) 변경 (MIME 타입이 필요)
            contentType: "application/json; charset=utf-8",
            dataType: "json"				// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
                                            // dataType : JSON => 생긴게 JSON이라면 => javascript로 오브젝트 변경 
        }).done(function(resp){					    
            // 응답의 결과가 정상이면 done을 실행되는 영역
            console.log(resp);
            alert("회원정보가 수정이 완료 되었습나다,");
            location.href = "/";			// 정상적으로 회원가입 후 다시 /blog url로 이동 
            
        }).fail(function(error){
            // 응답의 결과가 실패 하면 fail을 실행
            alert(JSON.spstringify(error));
        });
    },
}
index.init();