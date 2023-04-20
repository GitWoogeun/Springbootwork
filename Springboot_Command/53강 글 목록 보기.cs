#┌─────────────────────────────────────────────────────────────────────────────────
#│ 글 목록 보기
#└─────────────────────────────────────────────────────────────────────────────────


#┌─────────────────────────────────────────────────────────────────────────────────
#│ index.jsp의 글 목록 보여주기
#└─────────────────────────────────────────────────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
<%@ include file="layout/header.jsp"%>

// div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
<div class="container">

//items는 boardController에서 Model에 담아서 보낸 데이터를 한건씩 var board에 담아서 뿌릴수 있다.
<c:forEach var="board"  items="${boards}">
	<div class="card m-2">
		<div class="card-body">
            // 실제로는 board.getTitle()이 호출
			<h4 class="card-title">${board.title}</h4>
			<a href="#" class="btn btn-primary">상세보기</a>
		</div>
	</div>
</c:forEach>
	
</div>
// div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
<%@ include file="layout/footer.jsp"%>

#┌─────────────────────────────────────────────────────────────────────────────────
#│ Board Controller
#└─────────────────────────────────────────────────────────────────────────────────
// @Controller는 리턴할 때 ViewResolve가 작동 !! 해당 index 페이지로 Model을 들고 이동합니다.
@Controller
public class boardController {
	
	@Autowired
	private BoardService boardService;
	
	// 컨트롤러에서 어떻게 찾지?
	// @AuthenticationPrincipal PrincipalDetail principal => 유저의 권한 인증
	@GetMapping({"" , "/"})					
	public String index(Model model) {
        // model => jsp에서 보면 Request 정보 ( View 화면까지 )
        model.addAttribute("boards", boardService.글목록());   
		return "index";
	}
	
	// USER 권한이 필요
	@GetMapping("/board/saveForm")
	public String saveForm() {
		return "board/saveForm";
	}
}

#┌─────────────────────────────────────────────────────────────────────────────────
#│ Board Service
#└─────────────────────────────────────────────────────────────────────────────────
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
    
    // board 테이블에 있는 모든 데이터를 가져온다. findAll() 함수
    @Transactional
    public List<Board> 글목록() {
            return boardRepository.findAll();
	}
}