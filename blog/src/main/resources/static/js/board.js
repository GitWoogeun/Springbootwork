// 제이쿼리 사용
let index = {
		init: function() {
			// 글쓰기
			$("#btn-save").on("click", ()=>{
					this.save();		// <= 회원가입 버튼을 클릭 했을 시 save: function()을 호출
			});
			
			// 글 삭제
			$("#btn-delete").on("click", ()=>{
					this.deleteById();		// <= 회원가입 버튼을 클릭 했을 시 save: function()을 호출
			});
		},
		
		// 글 쓰기
		save: function(){
			// alert("user의 save함수 호출됨");
			let data = {
				title: $("#title").val(),
				content: $("#content").val(),						
			};
			// 데이터를 잘 가져오는지 확인
			// console.log(data);
			
			$.ajax({
				type: "post",									// POST방식으로 전송
				url: "/api/board",												
				data: JSON.stringify(data),		// JSON 문자열로 data ( http body 데이터 ) 변경 (MIME 타입이 필요)
				contentType: "application/json; charset=utf-8",
				dataType: "json"							// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
																		// dataType : JSON => 생긴게 JSON이라면 => javascript로 오브젝트 변경 
			}).done(function(resp){					    
				// 응답의 결과가 정상이면 done을 실행되는 영역
				console.log(resp);
				alert("글쓰기가 완료 되었습니다.");
				location.href = "/";				 
				
			}).fail(function(error){
				// 응답의 결과가 실패 하면 fail을 실행
				alert(JSON.spstringify(error));
			});
		},
		
		// 글 삭제
		deleteById: function(){
			
			// var id = $("#id").val()가 안돼는 이유
			// val()값을 가져오는게 아닌 text()값을 가져와야하기 때문에
			var id = $("#id").text();
			
			$.ajax({
				type: "delete",								// POST방식으로 전송
				url: "/api/board/"+id,																		
				contentType: "application/json; charset=utf-8",
				dataType: "json"							// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
			}).done(function(resp){					    
				// 응답의 결과가 정상이면 done을 실행되는 영역
				console.log(resp);
				alert("글 삭제가 완료 되었습니다.");
				location.href = "/";				 
			}).fail(function(error){
				// 응답의 결과가 실패 하면 fail을 실행
				alert("글 삭제가 되지 않았습니다.");
			});
		},
}

index.init();
