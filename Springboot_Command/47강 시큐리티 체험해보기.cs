#┌───────────────────────────────────────
#│ 스프링 시큐리티를 사용하기 위한 pom.xml
#└───────────────────────────────────────
<!-- 스프링 시큐리티 -->
<dependency>
    <groupId>org.springframework.security</groupId>
    <artifactId>spring-security-taglibs</artifactId>
</dependency>
<dependency>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-security</artifactId>
</dependency>
<dependency>
    <groupId>org.springframework.security</groupId>
    <artifactId>spring-security-test</artifactId>
    <scope>test</scope>
</dependency>

#┌───────────────────────────────────────
#│ 스프링 시큐리티를 사용하기 위한 pom.xml
#└───────────────────────────────────────
스프링 시큐리티를 설치하면 모든 대문이 닫힌다.
그래서 user와 패스워드를 입력하면 된다.

ID : user
PW : Using generated security password: 7d01350e-dff1-436b-9e4b-9101a701f105
로그인을 하고 난뒤 Spring Session이 실행 된다.

# 스프링시큐리티 태그 lib
<%@ taglib prefix="sec" uri="http://www.springframework.org/security/tags" %>
<!-- 스프링 시큐리티 사용  -->
<sec:authorize access="isAuthenticated()">
	<sec:authentication property="principal" var="principal"/>
</sec:authorize>

# SpringBoot Security가 실행이 되면 
# 자동적으로 잠기게 되는데 이때 스프링이 Session을 만들어서 저장을 해줘요
# 그 값ㅇ은 principal에 있어요!

┌───────────────────────────────────────
│ Spring Security을 적용한 header.jsp
└───────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>
<%@ taglib prefix="sec" uri="http://www.springframework.org/security/tags" %>

# 스프링 시큐리티 사용
<sec:authorize access="isAuthenticated()">
	<sec:authentication property="principal" var="principal"/>
</sec:authorize>

<!DOCTYPE html>
<html lang="en">
<head>
<title>Main_Index</title>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css">

<!-- jquery 관련 라이브러리는 위에다가 붙여 놓는게 좋다.  -->
<!-- 진심 정말 중요 :: ajax 사용시 3.5.1/jquery.min.js를 사용해야 한다.  -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>
</head>
<body>

	<nav class="navbar navbar-expand-md bg-dark navbar-dark">
		<a class="navbar-brand" href="/">RNB</a>
		<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#collapsibleNavbar">
			<span class="navbar-toggler-icon"></span>
		</button>
		<div class="collapse navbar-collapse" id="collapsibleNavbar">
			<c:choose>
            # Spring Security principal이 비어있다면 ( 로그아웃 )
				<c:when test="${empty principal }">
					<ul class="navbar-nav">
						<li class="nav-item"><a class="nav-link" href="/user/loginForm">로그인</a></li>
						<li class="nav-item"><a class="nav-link" href="/user/joinForm">회원가입</a></li>
					</ul>
				</c:when>
				<c:otherwise>
            # Spring Security principal이 비어있지 않다면 ( 로그인 )
					<ul class="navbar-nav">
						<li class="nav-item"><a class="nav-link" href="/board/form">글쓰기</a></li>
						<li class="nav-item"><a class="nav-link" href="/user/form">회원정보</a></li>
						<li class="nav-item"><a class="nav-link" href="/logout">로그아웃</a></li>
					</ul>
				</c:otherwise>
			</c:choose>
		</div>
	</nav>
	<br />