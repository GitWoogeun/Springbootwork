// 제이쿼리 사용
let index = {
		init: function() {
			// on : 첫번재  파라미터를 결정하고 클릭이 되면 두번째 파라미터가 무엇을할지 정하면 된다.
			$("#btn-save").on("click", ()=>{
					this.save();				
			});
		},
		
		save: function(){
			// alert("user의 save함수 호출됨");
			let data = {
				username: $("#username").val(),					// form태그에 있는 태그의 id값을 찾아서 username 변수에 값을 바인딩 한다.
				password: $("#password").val(),						// form태그에 있는 태그의 id값을 찾아서 password  변수에 값을 바인딩 한다.
				email: $("#email").val()										// form태그에 있는 태그의 id값을 찾아서 email          변수에 값을 바인딩 한다.
			}
			// 데이터를 잘 가져오는지 확인
			// console.log(data);
			
			$.ajax().done.fail();    // ajax 통신을 이용해서 3개의 데이터를 JSON으로 변경하여 insert 요청!!!
		}
}

index.init();