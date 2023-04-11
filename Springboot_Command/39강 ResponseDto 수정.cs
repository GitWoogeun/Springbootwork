
# ResponseDto 수정
# 버그 수정

//모든 Exception이 발생하면 GlobalExceptionHandler 클래스로 들어온다.
@ControllerAdvice   
@RestController
public class GlobalExceptionHandler {
	
    // Spring이 모든 Exception을 받아서 GlobalExceptionHandler 클래스에 보내준다.
    @ExceptionHandler(value = Exception.class)
    public ResponseDto<String> handleArgumentException( Exception e ) {
        return new ResponseDto<String>(HttpStatus.OK.value(), e.getMessage());
    }
}

// JSON이니까 @RequestBody로 파라미터 받음
// 통신상태를 확인하기 위해 HttpStatus.OK 
@PostMapping("/api/user")
public ResponseDto<Integer> save(@RequestBody User user) {		
    System.out.println("UserApiController : save 호출됨!");
    
    // username, password, email 입력값 form태그에서 받음
    // role은 back단에서 직접 넣어줘야함
    user.setRole(RoleType.USER);
    
    // 실제로 여기서 DB에 insert를 하고 아래에서 return이 되면 되요.
    userService.회원가입(user);
    
    // 회원가입이 성공 시 [ 상태값 : 200, 1 ] 호출
    // 자바 오브젝트를 JSON으로 변환해서 리턴 (Jackson)
    // 상태의 메시지를 호출
    return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);	
}