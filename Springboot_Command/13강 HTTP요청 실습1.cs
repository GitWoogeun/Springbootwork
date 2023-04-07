
# HTTP 요청 실습1

Controller 생성 및 실습

// 사용자가 요청을 했을 때 -> 응답(Data)   @RestController
// 사용자가 요청을 했을 때 -> 응답(Html)   @Controller
@RestController
public class HttpControllerTest {
	
    // 인터넷 브라우저 요청은 무조건 GET요청 밖에 할수가 없다.
	// http://localhost:8080/http/get	 ( SELECT )
	@GetMapping("/http/get")
	public String getTest() {
			return "get요청";
	}
	// http://localhost:8080/http/post   ( INSERT )
	@PostMapping("/http/post")
	public String postTest() {
			return "post요청";
	}
	// http://localhost:8080/http/put    ( UPDATE )
	@PutMapping("/http/put")
	public String putTest() {
			return "put요청";
	}
	//http://localhost:8080/delete		    ( DELETE )
	@DeleteMapping("/http/delete")
	public String deleteTest() {
			return "delete요청";
	}
}

404 : 페이지를 찾을수없다 (html or jsp)
405 : 해당 메서드가 허용되지 않는다.