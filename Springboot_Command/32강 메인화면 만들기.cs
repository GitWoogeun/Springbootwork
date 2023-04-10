


# boardController와 userController 클래스 생성

@Controller
public class boardController {

	@GetMapping({"" , "/"})		//아무것도 적지 않았을때랑 /를 붙였을때랑
	public String index() {
		//		/WEB-INF/views/index.jsp
		return "index";
	}
}



# Main화면 구현
src\main\webapp\WEB-INF\views\index.jsp파일 생성

<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html lang="en">
<head>
<title>Main_Index</title>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css">
<script src="https://cdn.jsdelivr.net/npm/jquery@3.6.3/dist/jquery.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>
</head>
<body>

	<nav class="navbar navbar-expand-md bg-dark navbar-dark">
		<a class="navbar-brand" href="/blog">RNB</a>
		<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#collapsibleNavbar">
			<span class="navbar-toggler-icon"></span>
		</button>
		<div class="collapse navbar-collapse" id="collapsibleNavbar">
			<ul class="navbar-nav">
				<li class="nav-item"><a class="nav-link" href="/user/login">로그인</a></li>
				<li class="nav-item"><a class="nav-link" href="/user/join">회원가입</a></li>
			</ul>
		</div>
	</nav>
	<br>

	<div class="container">
	
		<div class="card m-2" >
			<div class="card-body">
				<h4 class="card-title">제목 적는 부분</h4>
				<p class="card-text">내용 적는 부분</p>
				<a href="#" class="btn btn-primary">상세보기</a>
			</div>
		</div>
		
		<div class="card m-2" >
			<div class="card-body">
				<h4 class="card-title">제목 적는 부분</h4>
				<p class="card-text">내용 적는 부분</p>
				<a href="#" class="btn btn-primary">상세보기</a>
			</div>
		</div>
		
		<div class="card m-2" >
			<div class="card-body">
				<h4 class="card-title">제목 적는 부분</h4>
				<p class="card-text">내용 적는 부분</p>
				<a href="#" class="btn btn-primary">상세보기</a>
			</div>
		</div>
	</div>
	<div class="jumbotron text-center" style="margin-bottom: 0">
		<p _msttexthash="78208" _msthash="27">🏛 Company by RNB</p>
		<p _msttexthash="78208" _msthash="27">📞 010-1111-2222</p>
		<p _msttexthash="78208" _msthash="27">🏴 경기도 성남시 오리동</p>
	</div>
</body>
</html>