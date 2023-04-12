// 제이쿼리 사용
let index = {
		init: function() {
			// on : 첫번재  파라미터를 결정하고 클릭이 되면 두번째 파라미터가 무엇을할지 정하면 된다.
			// => : 화살표 함수 : function() {} / 대신 () => {} 사용한 이유는 this를 바인딩하기 위해서 사용
			$("#btn-save").on("click", ()=>{
					this.save();		// <= 회원가입 버튼을 클릭 했을 시 save: function()을 호출
			});
			$("#btn-login").on("click", ()=>{
					this.login();		// <= 회원가입 버튼을 클릭 했을 시 save: function()을 호출
			});
		},
		
		save: function(){
			// alert("user의 save함수 호출됨");
			let data = {
				username: $("#username").val(),					// form태그에 있는 태그의 id값을 찾아서 username 변수에 값을 바인딩 한다.
				password: $("#password").val(),						// form태그에 있는 태그의 id값을 찾아서 password  변수에 값을 바인딩 한다.
				email: $("#email").val()										// form태그에 있는 태그의 id값을 찾아서 email          변수에 값을 바인딩 한다.
			};
			// 데이터를 잘 가져오는지 확인
			// console.log(data);
			
			// ajax 통신을 이용해서 3개의 데이터를 JSON으로 변경하여 insert 요청!!!
			// ajax 호출 시 default가 비동기 호출
			// 회원가입 수행 요청 ( 100초 가정 ) 작업중이라고 해도 // fail 함수 밑에 프로세스를 실행함
			// ajax가 통신을 성공하고 나서 서버가 json을 리턴해주면 자동으로 자바 오브젝트로 변환해주네요 
			$.ajax({
				type: "post",									// POST방식으로 전송
				url: "/blog/api/user",					// UserApiController의 save() 함수 호출					
				data: JSON.stringify(data),		// JSON 문자열로 data ( http body 데이터 ) 변경 (MIME 타입이 필요)
				contentType: "application/json; charset=utf-8",
				dataType: "json"							// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
																		// dataType : JSON => 생긴게 JSON이라면 => javascript로 오브젝트 변경 
			}).done(function(resp){					    
				// 응답의 결과가 정상이면 done을 실행되는 영역
				console.log(resp);
				alert("회원가입이 완료 되었습니다.");
				location.href = "/blog";				// 정상적으로 회원가입 후 다시 /blog url로 이동 
				
			}).fail(function(error){
				// 응답의 결과가 실패 하면 fail을 실행
				alert(JSON.spstringify(error));
			});
		},
		
		// 로그인 기능 구현
		login: function(){
			alert("로그인이 되었습니다.");
			let data = {
				username: $("#username").val(),					// form태그에 있는 태그의 id값을 찾아서 username 변수에 값을 바인딩 한다.
				password: $("#password").val(),						// form태그에 있는 태그의 id값을 찾아서 password  변수에 값을 바인딩 한다.
			};
			
			$.ajax({
				type: "post",									// POST방식으로 전송
				url: "/blog/api/user/login",		// UserApiController의 save() 함수 호출					
				data: JSON.stringify(data),		// JSON 문자열로 data ( http body 데이터 ) 변경 (MIME 타입이 필요)
				contentType: "application/json; charset=utf-8",
				dataType: "json"							// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
																		// dataType : JSON => 생긴게 JSON이라면 => javascript로 오브젝트 변경 
			}).done(function(resp){					    
				// 응답의 결과가 정상이면 done을 실행되는 영역
				console.log(resp);
				alert("로그인이 완료 되었습니다.");
				location.href = "/blog";				// 로그인이 정상적으로 성공하면 main페이지로 이동
			}).fail(function(error){
				// 응답의 결과가 실패 하면 fail을 실행
				alert(JSON.spstringify(error));
			});
	    }
}

index.init();
