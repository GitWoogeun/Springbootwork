# 번외로 jquery를 불러올 떄 jquery 버전이 엄청 중요!
# ┌────────────────────────────────────────────────────────────
# │ header.jsp 영역 ( <script> 태그로 jquery 호출할 시 주요사항 )
# └────────────────────────────────────────────────────────────
<!-- jquery 관련 라이브러리는 위에다가 붙여 놓는게 좋다.  -->
<!-- 진심 정말 중요 :: ajax 사용시 3.5.1/jquery.min.js를 사용해야 한다.  -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

# ┌────────────────────────────────────────────────────────────
# │ UserApiController 영역
# └────────────────────────────────────────────────────────────
// 얘는 나중에 웹에도 쓸수 있다.
//데이터만 리턴을 해주는 컨트롤러 이기 때문에 @RestController
@RestController						
public class UserApiController {
	
    // JSON이니까 @RequestBody로 파라미터 받음
    // 통신상태를 확인하기 위해 HttpStatus.OK 
    @PostMapping("/api/user")
    public ResponseDto<Integer> save(@RequestBody User user) {
        System.out.println("UserApiController : save 호출됨!");
        
        // 실제로 여기서 DB에 insert를 하고 아래에서 return이 되면 되요.
        
        
        // 회원가입이 성공 시 [ 상태값 : 200, 1 ] 호출
        // 자바 오브젝트를 JSON으로 변환해서 리턴 (Jackson)
        return new ResponseDto<Integer>(HttpStatus.OK, 1);	
    }
}
# ┌────────────────────────────────────────────────────────────
# │ user.js 영역
# └────────────────────────────────────────────────────────────
// 제이쿼리 사용
let index = {
    init: function() {
        // on : 첫번재  파라미터를 결정하고 클릭이 되면 두번째 파라미터가 무엇을할지 정하면 된다.
        // => : 화살표 함수 : function() {} / 대신 () => {} 사용한 이유는 this를 바인딩하기 위해서 사용
        $("#btn-save").on("click", ()=>{
                this.save();
        });
    },
    
    save: function(){
        // alert("user의 save함수 호출됨");
        let data = {
            username: $("#username").val(),	    // form태그에 있는 태그의 id값을 찾아서 username 변수에 값을 바인딩 한다.
            password: $("#password").val(),		// form태그에 있는 태그의 id값을 찾아서 password 변수에 값을 바인딩 한다.
            email: $("#email").val()			// form태그에 있는 태그의 id값을 찾아서 email    변수에 값을 바인딩 한다.
        };
        // 데이터를 잘 가져오는지 확인
        // console.log(data);
        
        // ajax 통신을 이용해서 3개의 데이터를 JSON으로 변경하여 insert 요청!!!
        // ajax 호출 시 default가 비동기 호출
        // 회원가입 수행 요청 ( 100초 가정 ) 작업중이라고 해도 // fail 함수 밑에 프로세스를 실행함
        // ajax가 통신을 성공하고 나서 서버가 json을 리턴해주면 자동으로 자바 오브젝트로 변환해주네요 
        $.ajax({
            type: "post",
            url: "/blog/api/user",          //
            data: JSON.stringify(data),		// JSON 문자열로 data ( http body 데이터 ) 변경 (MIME 타입이 필요)
            contentType: "application/json; charset=utf-8",
            dataType: "json"				// 요청을 서버로해서 응답이 왔을 때 기본적으로 모든 것이 버퍼로 오기 때문에 응답값은 String 입니다.
                                            // dataType : JSON => 생긴게 JSON이라면 => javascript로 오브젝트 변경 
        }).done(function(resp){					    
            // 응답의 결과가 정상이면 done을 실행되는 영역
            console.log(resp);
            alert("회원가입이 완료 되었습니다.");
            location.href = "/blog";		// 정상적으로 회원가입 후 다시 /blog url로 이동 
            
        }).fail(function(error){
            // 응답의 결과가 실패 하면 fail을 실행
            alert(JSON.spstringify(error));
        });
    }
}
index.init();
