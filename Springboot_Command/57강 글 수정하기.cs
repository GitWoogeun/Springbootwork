#┌─────────────────────────────────────────────────────────────────────────────────
#│ 글 수정하기
#└─────────────────────────────────────────────────────────────────────────────────

#┌─────────────────────────────────────────────────────────────────────────────────
#│ updateForm.jsp
#└─────────────────────────────────────────────────────────────────────────────────
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
// 위치가 board이기 때문에 한칸 뒤로 가서 loayout/header.jsp로 이동해야 한다.
<%@ include file="../layout/header.jsp"%>
// div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
<div class="container">
	
	<form action="/auth/loginProc" method="post">
		<input type="hidden" id="id" value="${board.id}">
		<div class="form-group">
			 <input value="${board.title}" type="text" placeholder="Enter title" class="form-control"  id="title">
		</div>
		
		<div class="form-group">
  			<textarea  class="form-control summernote" rows="5"  id="content">
  				${board.content}
  			</textarea>
		</div>
	</form>
	<button id="btn-update" class="btn btn-primary">글 수정하기</button>
	<button class="btn btn-info" onclick="history.back()">돌아가기</button>
</div>

// summer note 글 내용(textarea)을 꾸며주기 위해 사용
<script>
      $('.summernote').summernote({
        tabsize: 2,
        height: 300
      });
    </script>

// js 폴더 / board.js 파일 연결
<script src="/js/board.js"></script>
// div class container : container는 header 영역과 footer 영역 안에 포함되어있는 공간
<%@ include file="../layout/footer.jsp"%>


#┌─────────────────────────────────────────────────────────────────────────────────
#│ BoardController
#└─────────────────────────────────────────────────────────────────────────────────
@Controller
public class boardController {
	
	@Autowired
	private BoardService boardService;
	
	// 메인화면 이동
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
	
	// 회원가입 화면 이동
	// USER 권한이 필요
	@GetMapping("/board/saveForm")
	public String saveForm() {
		return "board/saveForm";
	}
	
	// 글 상세보기 화면 이동
	@GetMapping("/board/{id}")
	public String findById(@PathVariable int id, Model model) {
		model.addAttribute("board", boardService.글상세보기(id));
		
		return "board/detail";
	}
	
	// 수정화면 이동
	@GetMapping("/board/{id}/updateForm")
	public String updateForm(@PathVariable int id, Model model){
		model.addAttribute("board", boardService.글상세보기(id));
		return "board/updateForm";
	}
}

#┌─────────────────────────────────────────────────────────────────────────────────
#│ BoardApiController
#└─────────────────────────────────────────────────────────────────────────────────
// 얘는 나중에 웹에도 쓸수 있다.
//데이터만 리턴을 해주는 컨트롤러 이기 때문에 @RestController
@RestController						
public class BoardApiController {

    @Autowired
    private BoardService boardService;

    // JSON이니까 @RequestBody로 파라미터 받음
    // 통신상태를 확인하기 위해 HttpStatus.OK 
    // board.js의 $Ajax에게 데이터 요청
    @PostMapping("/api/board")
    public ResponseDto<Integer> save(@RequestBody Board board, @AuthenticationPrincipal PrincipalDetail principal) {		
        
        boardService.글쓰기(board, principal.getUser());
        
        // 회원가입이 성공 시 [ 상태값 : 200, 1 ] 호출
        // 자바 오브젝트를 JSON으로 변환해서 리턴 (Jackson)
        return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);	
    }
    
    @DeleteMapping("/api/board/{id}")
    public ResponseDto<Integer> deleteById(@PathVariable int id){
        boardService.글삭제하기(id);
        return new ResponseDto<Integer>(HttpStatus.OK.value(), 1);
    }
    
    // Delete 랑 URL 주소가 같아도 상관없다 요청 Method가 다르기 때문에
    // 시큐리티의 ID와 Board의 정보를 매개변수로 보낸다.
    // @RequestBody를 사용하는 이유 : updateForm.jsp에 데이터를 보내기 위해서
    @PutMapping("/api/board/{id}")
    public ResponseDto<Integer> update(@PathVariable int id, @RequestBody Board board) {
        boardService.글수정하기(id, board);
        return new ResponseDto<Integer>(HttpStatus.OK.value(),1);
    }
}

#┌─────────────────────────────────────────────────────────────────────────────────
#│ Board.js
#└─────────────────────────────────────────────────────────────────────────────────
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
        
        // 글 수정
        $("#btn-update").on("click", ()=>{
                this.update();		// <= 회원가입 버튼을 클릭 했을 시 save: function()을 호출
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
        let id = $("#id").text();
        
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
    
    // 글 수정
    update: function(){
        let id = $("#id").val();
        
        let data = {
            title: $("#title").val(),
            content: $("#content").val(),			
        };
        
        // 데이터를 잘 가져오는지 확인
        // console.log(data);
        $.ajax({
            type: "put",									// POST방식으로 전송
            url: "/api/board/"+id,												
            data: JSON.stringify(data),		// JSON 문자열로 data ( http body 데이터 ) 변경 (MIME 타입이 필요)
            contentType: "application/json; charset=utf-8",
            dataType: "json"							// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
                                                                    // dataType : JSON => 생긴게 JSON이라면 => javascript로 오브젝트 변경 
        }).done(function(resp){					    
            // 응답의 결과가 정상이면 done을 실행되는 영역
            console.log(resp);
            alert("글 수정이 완료 되었습니다.");
            location.href = "/";				 
            
        }).fail(function(error){
            // 응답의 결과가 실패 하면 fail을 실행
            alert(JSON.spstringify(error));
        });
    },
}

index.init();
