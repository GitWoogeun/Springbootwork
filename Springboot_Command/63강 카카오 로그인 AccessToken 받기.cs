#┌─────────────────────────────────────────────────────────────────────────────────
#│ 카카오 로그인 AccessToken 받기
#└─────────────────────────────────────────────────────────────────────────────────

들어가기전
#┌─────────────────────────────────────────────────────────────────────────────────
#│ Header와 Body의 개념
#└─────────────────────────────────────────────────────────────────────────────────

HTTP 헤더 : 
=> 클라이언트와 서버가 주고 받는 메시지의 메타데이터를 포함하는 부분 입니다.

헤더의 정보 {
    headers.add("Content-type", "application/x-www-form-urlencoded;charset=utf-8");
}

#┌─────────────────────────────────────────────────────────────────────────────────
#│ [ HTTPHeaders 객체 ]
#└─────────────────────────────────────────────────────────────────────────────────
=> HTTP 요청 또는 응답에서 사용되는 HTTP 헤더를 저장하는데 사용
=> 일반적으로 딕셔너리와 비슷한 형태를 가지며, 각 헤더 필드에 대한 ( key : value ) 포함

Headers 메서드 {
    add(field, value)       : 주어진 필드와 값을 HTTPHeaders 객체에 추가
    get(field, default=None): 주어진 필드의 값을 반환합니다.
    keys()                  : HTTPHeaders 객체에 있는 모든 키를 반환
    values()                : HTTPHeaders 객체에 있는 모든 값을 반환
    items()                 : HTTPHeaders 객체에 있는 모든 키-값 쌍을 반환
    remove(field)           : 주어진 필드를 HTTPHeaders 객체에서 제거
}

HTTP 바디 :
=> HTTP 요청 메시지의 경우, HTTP 본문은 클라이언트에서 서버로 보내는 데이터
=> HTTP 응답 메시지의 경우, HTTP 본문은 서버에서 클라이언트로 보내는 데이터
=> HTML, JSON, XML, 이미지, 비디오 등 다양한 데이터 형식을 포함;

#┌─────────────────────────────────────────────────────────────────────────────────
#│ [ MultiValueMap 객체 ]
#└─────────────────────────────────────────────────────────────────────────────────
=> MultiValueMap은 하나의 키에 대해 여러 개의 값을 저장할 수 있는 자료 구조

=> Spring 프레임워크에서 사용되는 MultiValueMap은 Map 인터페이스를 상속받아 구현되었으며, 
   하나의 키에 대해 여러 개의 값을 저장

=> HTTP 요청에서 파라미터를 추출할 때, MultiValueMap<String, String>을 사용하여 
   파라미터를 저장하고 추출

MultiValueMap {
    add(field, value)       : 주어진 필드와 값을 HTTPHeaders 객체에 추가
    get(field, default=None): 주어진 필드의 값을 반환합니다.
    keys()                  : HTTPHeaders 객체에 있는 모든 키를 반환
    values()                : HTTPHeaders 객체에 있는 모든 값을 반환
    items()                 : HTTPHeaders 객체에 있는 모든 키-값 쌍을 반환
    remove(field)           : 주어진 필드를 HTTPHeaders 객체에서 제거
}

#┌─────────────────────────────────────────────────────────────────────────────────
#│ [ LinkedMultiValueMap 객체 ]
#└─────────────────────────────────────────────────────────────────────────────────
=> LinkedMultiValueMap은 MultiValueMap 인터페이스를 구현한 클래스 중 하나

=> MultiValueMap 인터페이스에서 정의된 메서드를 모두 구현하며, 하나의 키에 대해 
   여러 값을 가질 수 있는 맵을 구현

=> LinkedMultiValueMap은 키에 대한 여러 값을 저장하고 조회하는 데 용이한 자료 구조

LinkedMultiValueMap {
    add(K key, V value): 지정된 키에 지정된 값 하나를 추가
    addAll(K key, List<? extends V> values): 지정된 키에 지정된 값 리스트를 추가
    set(K key, V value): 지정된 키에 지정된 값 하나를 설정
    setAll(Map<K, List<V>> values): 지정된 맵에 있는 모든 값을 설정
}


[ RestTemplate 객체 ]
=> RestTemplate을 사용하면 HTTP 요청을 보내고, HTTP 응답을 받아서 처리할 수 있습니다. ;

#┌─────────────────────────────────────────────────────────────────────────────────
#│ [ 카카오 로그인 ] UserController
#└─────────────────────────────────────────────────────────────────────────────────
// 카카오 로그인
// @ResponseBody를 붙이면 데이터를 리턴해주는 함수가 되는거에요~
@GetMapping("/auth/kakao/callback")
public @ResponseBody String kakaoCallback(String code) {
        
    // POST 방식으로 key = value 타입의 데이터를 요청 ( 카카오 쪽으로 )
    RestTemplate rt = new RestTemplate();
    
    // HttpHeader 오브젝트 생성 ( Header의 정보를 담을 Headers )
    HttpHeaders headers = new HttpHeaders();
    headers.add("Content-type", "application/x-www-form-urlencoded;charset=utf-8");
    
    // HttpBody 오브젝트 생성 ( Body의 정보를 담을 MultiValueMap )
    MultiValueMap<String, String> params = new LinkedMultiValueMap<>();
    params.add("grant_type"   , "authorization_code");
    params.add("client_id"    , "3aaf6213852724e22060920d2d73c05b");
    params.add("redirect_uri" , "http://localhost:8000/auth/kakao/callback");
    params.add("code"         , code);
    
    // HttpHeader와 HttpBody를 하나의 오브젝트에 담기 ( 이유 : rt.exchange 함수가 HttpEntity 오브젝트를 넣게 되어있어서 )
    HttpEntity<MultiValueMap<String, String>> kakaoTokenRequest = 
                new HttpEntity<>(params, headers);
    
    // Http 요청하기 - Post 방식으로 - 그리고 response 변수의 응답 받음.
    ResponseEntity<String> response = rt.exchange(
            "https://kauth.kakao.com/oauth/token",
            HttpMethod.POST,
            kakaoTokenRequest,
            String.class
    );
    
    return "카카오 토큰 요청 완료 : 토큰 요청에 대한 응답 = " + response;
}

#┌─────────────────────────────────────────────────────────────────────────────────
#│ [ 카카오 로그인 ] loginForm.jsp
#└─────────────────────────────────────────────────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
# 현재위치에서 (joinForm.jsp) layout폴더의 header.jsp를 찾을려면 한칸 위로 올라가서 찾아야 한다.
<%@ include file="../layout/header.jsp"%>
# div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
<div class="container">
	<form action="/auth/loginProc" method="post">
	<!-- User Name -->
		<div class="form-group">
			<label for="username">User Name :</label>
			 <input type="text" name="username" placeholder="Enter username" class="form-control"  id="username">
		</div>
	<!-- Password  -->
		<div class="form-group">
			<label for="password">User Password :</label> 
			<input type="password"  name="password" class="form-control" placeholder="Enter password" id="password">
		</div>
	<button id="btn-login" class="btn btn-primary">로그인</button>
	// 카카오 로그인 <a>태그
	<a href="https://kauth.kakao.com/oauth/authorize?client_id=3aaf6213852724e22060920d2d73c05b&redirect_uri=http://localhost:8000/auth/kakao/callback&response_type=code"><img height="38.5px" src="/image/kakao_login_button.png" ></a>
	</form>
</div>
// <script src="/js/user.js"></script>  -->
// div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
<%@ include file="../layout/footer.jsp"%>