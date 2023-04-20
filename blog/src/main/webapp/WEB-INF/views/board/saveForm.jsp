<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>

<!-- 위치가 board이기 때문에 한칸 뒤로 가서 loayout/header.jsp로 이동해야 한다. -->
<%@ include file="../layout/header.jsp"%>
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<div class="container">
	
	<form action="/auth/loginProc" method="post">
		<div class="form-group">
			<label for="title">제목</label>
			 <input type="text"  placeholder="Enter title" class="form-control"  id="title">
		</div>
		
		<div class="form-group">
  			<label for="content">글 내용</label>
  			<textarea  class="form-control summernote" rows="5"  id="content">
  			
  			</textarea>
		</div>
	</form>
	<button id="btn-save" class="btn btn-primary">글쓰기</button>
</div>

<script>
      $('.summernote').summernote({
        tabsize: 2,
        height: 300
      });
    </script>

<!-- js 폴더 / user.js 파일 연결  -->
<script src="/js/board.js"></script>
<!--  div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간 -->
<%@ include file="../layout/footer.jsp"%>
