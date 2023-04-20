<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<!-- 현재위치에서 (joinForm.jsp) layout폴더의 header.jsp를 찾을려면 한칸 위로 올라가서 찾아야 한다. -->
<%@ include file="../layout/header.jsp"%>
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
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
	</form>
</div>
<!-- <script src="/js/user.js"></script>  -->
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<%@ include file="../layout/footer.jsp"%>