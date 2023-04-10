
# 스프링 기본파싱전략과 json통신

# JSON 데이터로 통신하기

1. GET요청 
    => 주소에 데이터를 담아 보낸다 데이터 형태는 KEY = VALUE
    => http://localhost:8000/blog/dummy/user/1

    특징 :
        데이터 형태 : key = value
        body로 데이터를 담아 보내지 않음

2. Post, Put, Delete 요청 ( 데이터를 변경 )
    => 데이터를 담아 보내야 할 것이 많음.
    => username, password, address, gender, createDate
    => Post {
            form 태그 method = "post" 방식으로 보내면 된다.
            form 태그의 한계 -> get요청과 post요청만 할수있다.
       }

    => Put, delete 요청은 자바스크립트로 요청을 해야 함! 
        => 데이터 형태 ( Key : Value )

# 통일 : 자바스크립트 ajax요청 + 데이터는 json으로 통일!!
    => 이렇게 된다면 post, put, delete를 따로 구분할 필요가 없다.;         사용 O


# 스프링 formform 태그가 있다.
        => post, put, delete요청도 가능하고 get요청도 가능하다.
        => 태그 형태 = <from:from></from:from>
        => <form:from method= "get, post, put, delete" 다 넣을 수 있다.> 사용 X


# 스프링 컨트롤러의 파싱 전략 1 : 함수 파싱
스프링 컨트롤러는 [ key = value 데이터를 자동으로 파싱하여 변수 ]에 담아준다.
가령 get 요청은 key = value이고, post 요청중에 x-www.form-urlencoded (from)태그를 만들어서
데이터 전송 시에도 key = value 이기 때문에 이러한 데이터는 아래와 같이 함수의 파라미터로 받을 수 있다.

key = value 형태로 데이터를 전송하면 함수의 파라미터로 받을 수 있음

PostMapping("/home")
public String home(String name, String email) {
    return "home";
}

# 스프링 컨트롤러의 파싱 전략 2 : 오브젝트 파싱 ( setter 필수! )
스프링은 key = value 형태의 데이터를 오브젝트로 파싱해서 받아주는 역할도 한다.
** 이때 주의 할점은 setter가 없으면 key = value 데이터를 스프링이 파싱해서 넣어주지 못한다.
    => 스프링이 setter를 호출해서 오브젝트를 만들어주기 때문에
    => 자동으로 setter가 없는건 스프링에서 걸러준다.

예시 ) 
위치 : User Class
User 클래스에 setter가 없을 시 스프링 컨트롤러의 파싱 전략이 안됨

위치 : UserContrller

@PostMapping("/home")
public String home (User user)  // <= User클래스에 setter가 없으면 파라미터 받지 못함!
{
    return "home";
}


3. 오브젝트로 데이터 받기
post 방식의 key : value 형태로 데이터를 보낼건데 ( x-www.form-urlencoded )방식
username = ssar  // key값하고 User클래스의 변수값하고 같아야한다.
password = 1234  // key값하고 User클래스의 변수값하고 같아야한다.
그렇지 않으면 파라미터 파싱 안됨;


4. key = value가 아닌 데이터는 어떻게 파싱을 할까?
json 데이터나 일반 text 데이터는 스프링 컨트롤러에서 받기 위해서
@RequestBody 어노테이션이 필요하다.
기본전략이 스프링 컨트롤러는 key = value 데이터를 파싱해서 받아주는 일을 하는데 
다른 형태의 데이터가령 json 같은 데이터는 아래와 같이 생겼다.
# JSON 형태의 데이터
{
    "username" : "rnbsoft",
    "password" : "1234567"
}
이런 데이터는 스프링이 파싱해서 오브젝트로 받지 못한다.
그래서 [ @RequestBody 어노테이션을 붙이면 스프링의 MessageConvert 클래스를 구현한
Jackson 라이브러리가 발동하면서 json 데이터를 자바 오브젝트로 파싱하여 받아준다. ]


5. form 태그로 json 데이터 요청 방법 
key = value 데이터가 아니라 json 데이터를 어떻게 전송할 수 있을까?

# join.jsp
{
    <div class="container">

	<form>
		<div class="form-group">
			<label for="username">유저네임</label> 
			<input type="text" id="username">
		</div>
		<div class="form-group">
			<label for="password">패스워드</label> 
			<input type="password" id="password">
		</div>
		
		<div class="form-group">
			<label for="email">이메일</label> 
			<input type="email" id="email">
		</div>
	</form>
	
	<button id="join--submit" class="btn btn-primary">회원가입</button>

</div>

<script src="/js/join.js"></script>
}

# join.js
{
<script>
$('#join--submit').on('click', function() {
	var data = {                                # 아래의 3개의 데이터를 data변수에 저장
		username : $('#username').val(),        # input태그의 id값의 username의 value값을 담고
		password : $('#password').val(),        # input태그의 id값의 password의 value값을 담고
		email : $('#email').val()               # input태그의 id값의 email의 value 값을 담고
	};

	$.ajax({
		type : 'POST',                // POST방식
		url : '/user/join',           // 해당 URL
		data : JSON.stringify(data),  // 자바스크립트 data를 JSON으로 변경해서 data변수에 저장
		contentType : 'application/json; charset=utf-8',  // application/json 타입이라고 알려주고
		dataType : 'json'
	}).done(function(r) {
		if (r.statusCode == 200) {
			console.log(r);
			alert('회원가입 성공');
			location.href = '/user/login';
		} else {
			if (r.msg == '아이디중복') {
				console.log(r);
				alert('아이디가 중복되었습니다.');
			} else {
				console.log(r);
				alert('회원가입 실패');
			}
		}
	}).fail(function(r) {
		var message = JSON.parse(r.responseText);
		console.log((message));
		alert('서버 오류');
	});
});
</script>
}