<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<!-- 현재위치에서 (joinForm.jsp) layout폴더의 header.jsp를 찾을려면 한칸 위로 올라가서 찾아야 한다. -->
<%@ include file="../layout/header.jsp"%>
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<div class="container">
	<form action="/action_page.php">
	<!-- User Name -->
		<div class="form-group">
			<label for="username">UserName :</label> <input type="text" placeholder="Enter username" class="form-control"  id="username">
		</div>
	<!-- Email  -->
		<div class="form-group">
			<label for="email">User Email :</label> <input type="email" class="form-control" placeholder="Enter email" id="email">
		</div>
	<!-- Password  -->
		<div class="form-group">
			<label for="password">User Password :</label> <input type="password" class="form-control" placeholder="Enter password" id="password">
		</div>
		<div class="form-group form-check">
			<label class="form-check-label"> <input class="form-check-input" type="checkbox"> Remember me
			</label>
		</div>
		<button type="submit" class="btn btn-primary">회원가입</button>
	</form>
</div>
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<%@ include file="../layout/footer.jsp"%>
