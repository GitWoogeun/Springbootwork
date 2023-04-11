
# 로그인, 회원가입 페이지 만들기

1. UserController를 하나 만들어서 Login과 Join에 화면 연결 Controller를 작성

[ UserController ]
package com.cos.blog.controller;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;

@Controller
public class UserController {
	
	@GetMapping("/user/joinForm")
	public String joinForm() {
		return "user/joinForm";
	}
	
	@GetMapping("/user/loginForm")
	public String loginForm() {
		return "user/loginForm";
	}
}

# ┌───────────────────────────
# │ footer와 header include 하기 
# └───────────────────────────

# ┌────────────────────────────────────────────────────────────────────
# │ header.jsp 영역 ( <%@ include file="layout/header.jsp"%> )
# └────────────────────────────────────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html lang="en">
<head>
<title>Main_Index</title>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css">
</head>
<body>
	<nav class="navbar navbar-expand-md bg-dark navbar-dark">
		<a class="navbar-brand" href="/blog">RNB</a>
		<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#collapsibleNavbar">
			<span class="navbar-toggler-icon"></span>
		</button>
		<div class="collapse navbar-collapse" id="collapsibleNavbar">
			<ul class="navbar-nav">
				<li class="nav-item"><a class="nav-link" href="/blog/user/loginForm">로그인</a></li>
				<li class="nav-item"><a class="nav-link" href="/blog/user/joinForm">회원가입</a></li>
			</ul>
		</div>
	</nav>
	<br/>
# ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
# ┌────────────────────────────────────────────────────────────────────
# │ index.jsp 영역 ( include를 사용하여 header와 footer 영역을 가져옴 )
# └────────────────────────────────────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>

<%@ include file="layout/header.jsp"%>
# div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
<div class="container">
	<div class="card m-2">
		<div class="card-body">
			<h4 class="card-title">제목 적는 부분</h4>
			<a href="#" class="btn btn-primary">상세보기</a>
		</div>
	</div>

	<div class="card m-2">
		<div class="card-body">
			<h4 class="card-title">제목 적는 부분</h4>
			<a href="#" class="btn btn-primary">상세보기</a>
		</div>
	</div>

	<div class="card m-2">
		<div class="card-body">
			<h4 class="card-title">제목 적는 부분</h4>
			<a href="#" class="btn btn-primary">상세보기</a>
		</div>
	</div>
</div>
<%@ include file="layout/footer.jsp"%> 
# ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
# ┌──────────────────────────────────────────────────────────
# │ footer.jsp 영역 ( <%@ include file = "layout/footer.jsp")
# └──────────────────────────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<br />
<div class="jumbotron text-center" style="margin-bottom: 0">
	<p _msttexthash="78208" _msthash="27">🏛 Company by RNB</p>
	<p _msttexthash="78208" _msthash="27">📞 010-1111-2222</p>
	<p _msttexthash="78208" _msthash="27">🏴 경기도 성남시 오리동</p>
</div>
<!-- HTML을 먼저 구현이 되고 밑에서 script가 실행이 되어 적용을 하기 때문에 밑에 다 두는것이 좋다.-->
<!-- script는 보통 body가 끝나는 가장 가까운곳에 넣어 둔다. HTML은 인터프립터 언어 -->
<script src="https://cdn.jsdelivr.net/npm/jquery@3.6.3/dist/jquery.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>

# ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
# ┌──────────────────────────────────────────────────────────
# │ joinForm.jsp 영역 ( 회원가입 하는 페이지 )
# └──────────────────────────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<!-- 현재위치에서 (joinForm.jsp) layout폴더의 header.jsp를 찾을려면 한칸 위로 올라가서 찾아야 한다. -->
<%@ include file="../layout/header.jsp"%>
# div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
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
<%@ include file="../layout/footer.jsp"%>
# ┌──────────────────────────────────────────────────────────
# │ loginForm.jsp 영역 ( 로그인 하는 페이지 )
# └──────────────────────────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<!-- 현재위치에서 (joinForm.jsp) layout폴더의 header.jsp를 찾을려면 한칸 위로 올라가서 찾아야 한다. -->
<%@ include file="../layout/header.jsp"%>
# div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
<div class="container">
	<form action="/action_page.php">
	<!-- User Name -->
		<div class="form-group">
			<label for="username">User Name :</label> <input type="text" placeholder="Enter username" class="form-control"  id="username">
		</div>
	<!-- Password  -->
		<div class="form-group">
			<label for="password">User Password :</label> <input type="password" class="form-control" placeholder="Enter password" id="password">
		</div>
		<div class="form-group form-check">
			<label class="form-check-label"> <input class="form-check-input" type="checkbox"> Remember me
			</label>
		</div>
		<button type="submit" class="btn btn-primary">로그인</button>
	</form>
</div>
<%@ include file="../layout/footer.jsp"%>