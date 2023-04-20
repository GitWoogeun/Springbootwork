#┌─────────────────────────────────────────────────────────────────────────────────
#│ 글 목록 페이징 하기
#└─────────────────────────────────────────────────────────────────────────────────


#┌─────────────────────────────────────────────────────────────────────────────────
#│ 페이징 처리 Controller 
#└─────────────────────────────────────────────────────────────────────────────────
@Controller
public class boardController {
	
	@Autowired
	private BoardService boardService;
	
	// 컨트롤러에서 어떻게 찾지?
	// @AuthenticationPrincipal PrincipalDetail principal
	// @PageableDefault => 페이징처리 ( SIZE = 보여주는 글 개수, SORT = 보여주는 기준, Sort.Direction.DESC = SORT 기준의 내림차순으로 정렬 ) 
	@GetMapping({"" , "/"})					
	public String index(Model model, @PageableDefault(size = 3, sort="id", direction = Sort.Direction.DESC) Pageable pageable) {
		
		// model => Jsp에서 보면 Request 정보 ( View 화면 까지 )	
		model.addAttribute("boards", boardService.글목록(pageable));   
		// @Controller는 리턴할 때 ViewResolve가 작동 !! 해당 index 페이지로 Model을 들고 이동합니다.
		return "index";			
		 
	}
	
	// USER 권한이 필요
	@GetMapping("/board/saveForm")
	public String saveForm() {
		return "board/saveForm";
	}
}

#┌─────────────────────────────────────────────────────────────────────────────────
#│ 페이징 처리 Service
#└─────────────────────────────────────────────────────────────────────────────────
package com.cos.blog.service;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.cos.blog.model.Board;
import com.cos.blog.model.User;
import com.cos.blog.repository.BoardRepository;

// 스프링이 컴포넌트 스캔을 통해서 빈에 등록을 해준다.  ( IoC를 해준다 ) 메모리를 대신 띄어준다.
@Service
public class BoardService {
	
    @Autowired
    private BoardRepository boardRepository;
    
    // javax의 @Transaction
    // 트랜잭션중 하나라도 실패를 한다면 커밋이 되지 않고 RollBack이 될겁니다.
    @Transactional
    public void 글쓰기(Board board, User user) {				// title, content
        board.setCount(0);
        board.setUser(user);
        boardRepository.save(board);
    }
    
    // Pageable을 리턴을 한다면 리턴 타입을 List가 아닌 Page로 해줘야한다.
    // Page를 리턴 타입으로 지정할 시 이게 첫번째 페이지 인지 마지막 페이지 인지 확인할수 있다.
    @Transactional
    public Page<Board> 글목록(Pageable pageable) {
            return boardRepository.findAll(pageable);
    }
}

<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>

<%@ include file="layout/header.jsp"%>

// div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
<div class="container">
	// items는 boardController에서 Model에 담아서 보낸 데이터를 한건씩 var board에 담아서 뿌릴수 있다.
	<c:forEach var="board" items="${boards.content}">
		<div class="card m-2">
			<div class="card-body">
				<h4 class="card-title">${board.title}</h4>
				// 실제로는 board.getTitle()이 호출
				<a href="#" class="btn btn-primary">상세보기</a>
			</div>
		</div>
	</c:forEach>
	// 페이징 버튼 생성 , ( justify-content-center 버튼을 가운대로 이동 시키는 Bootstrap 문법 )
	// 버튼을 왼쪽으로 이동 => start , 버튼을 가운대로 이동 => center, 버튼을 오른쪽으로 이동 => end
	<ul class="pagination justify-content-center">
		<c:choose>
			// 페이징 => 첫 번째 페이지 이면 이전 버튼 비활성화
			<c:when test="${boards.first}">
				<li class="page-item disabled"><a class="page-link" href="?page=${boards.number-1}">이전</a></li>
			</c:when>
			<c:otherwise>
				// 페이징 => 첫 번째 페이지가 아니라면 이전 버튼 활성화
				<li class="page-item"><a class="page-link" href="?page=${boards.number-1}">이전</a></li>
			</c:otherwise>
		</c:choose>
		<c:choose>
			// 페이징 => 마지막 페이지 이면 다음 버튼 비활성화
			<c:when test="${boards.last}">
				<li class="page-item disabled"><a class="page-link" href="?page=${boards.number+1}">다음</a></li>
			</c:when>
			<c:otherwise>
				// 페이징 => 마지막 페이지가 아니라면 다음 버튼 활성화
				<li class="page-item"><a class="page-link" href="?page=${boards.number+1}">다음</a></li>
			</c:otherwise>
		</c:choose>
	</ul>
</div>
// div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
<%@ include file="layout/footer.jsp"%>
