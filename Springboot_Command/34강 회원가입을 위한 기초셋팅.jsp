
# 자바스크립트 파일은 정적인 파일이기 때문에 static 폴더 안에 넣어둔다.

# ┌───────────────────────────────────────────────────────────────────────────────────
# │ joinForm.jsp 영역 ( form태그의 action 방식이 아닌 JSON (자바스크립트 방식으로 구현) )
# └───────────────────────────────────────────────────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<!-- 현재위치에서 (joinForm.jsp) layout폴더의 header.jsp를 찾을려면 한칸 위로 올라가서 찾아야 한다. -->
<%@ include file="../layout/header.jsp"%>
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<div class="container">
	<!-- form태그에 action을 타서 Controller의 Mapping 지역으로 호출  -->
	<!-- JSON 방식으로 (자바스크립트) 회원가입하기 -->
	<form>
	<!-- User Name Input 영역  -->
		<div class="form-group">
			<label for="username">UserName :</label> <input type="text" placeholder="Enter username" class="form-control"  id="username">
		</div>
	<!-- Password  Input 영역  -->
		<div class="form-group">
			<label for="password">User Password :</label> <input type="password" class="form-control" placeholder="Enter password" id="password">
		</div>
	<!-- Email  Input 영역 -->
		<div class="form-group">
			<label for="email">User Email :</label> <input type="email" class="form-control" placeholder="Enter email" id="email">
		</div>
		<div class="form-group form-check">
			<label class="form-check-label"> <input class="form-check-input" type="checkbox"> Remember me
			</label>
		</div>
	</form>
	<button id="btn-save" class="btn btn-primary">회원가입</button>
</div>

<!--js 폴더 연결 -->
<script src="/blog/js/user.js"></script>

<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<%@ include file="../layout/footer.jsp"%>
<!--
# ┌───────────────────────────────────────────────────────────────────────────────────
# │ javascript파일은 정적인 파일이므로 static 폴더 아래에 두어야 한다.
# └───────────────────────────────────────────────────────────────────────────────────-->
 
제이쿼리 사용
let index = {
	init: function() {
<!--on : 첫번재  파라미터를 결정하고 클릭이 되면 두번째 파라미터가 무엇을할지 정하면 된다.-->
		$("#btn-save").on("click", ()=>{
				this.save();				
		});
	},
	
	save: function(){
		<!-- alert("user의 save함수 호출됨"); -->
		let data = {
			username: $("#username").val(),		// form태그에 있는 태그의 id값을 찾아서 username 변수에 값을 바인딩 한다.
			password: $("#password").val(),		// form태그에 있는 태그의 id값을 찾아서 password  변수에 값을 바인딩 한다.
			email: $("#email").val()			// form태그에 있는 태그의 id값을 찾아서 email          변수에 값을 바인딩 한다.
		}
		// 데이터를 잘 가져오는지 확인
		// console.log(data);

		// ajax 통신을 이용해서 3개의 데이터를 JSON으로 변경하여 insert 요청!!!
		$.ajax().done.fail();    
	}
}
index.init();