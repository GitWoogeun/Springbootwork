
# Lombok 사용해보기

# Lombok 경로 :
C:\Users\탁우근\.m2\repository\org\projectlombok\lombok\1.18.26

# GitBash에서 Lombok 실행 명령어 :
$ java -jar lombok-1.18.26.jar
=> 현재 내가 사용하고 있는 Tool에다가 설정 


@Data						// getter와 setter 사용
@NoArgsConstructor			// 빈 생성자 사용
public class Member {
	// 자바에서 변수를 private으로 만드는 이유 :
	// => 자바는 OOP : 객체지향이기 때문에 캡슐화를 하기 위해
	// => 변수에 다이렉트로 연결해서 변수의 값을 변경할수 없게 하기 위해서 ( Java = 객체지향이니까 )
	// => 변수는 생성자를 통해서 값을 변경해줘야 한다.
	
	private int id;
	private String username;
	private String password;
	private String email;

    @Builder   // 생성자를 알아서 빌더 해준다.
	public Member(int id, String username, String password, String email) {
		this.id = id;
		this.username = username;
		this.password = password;
		this.email = email;
	}
}


@RestController
public class HttpControllerTest {
	
	// 문자열 생성 BATCH_ID처럼 클래스 이름을 알려주기 위해 선언 및 정의
	private static final String TAG = "HttpControllerTest : ";
	
	@GetMapping("/http/lombok")
	public String lombokTest() {
		// 빌더패턴을 사용 시 장점 : 내가 값을 넣는 순서를 지키지 않아도 된다.
		//                       : 생성자를 생성해서 짚어넣을때는 순서를 반드시 지켜야한다, (id, name, password, email ) 이 순서로
	    //					     : 빌드를 사용 시 생성자처럼 순서를 지키지 않아도 된다 빌더가 알아서 잡아준다.
		Member mem1 = Member.builder().username("rnb").password("1234").email("rnb@rnb.com").build();
		System.out.println(TAG + "getter : " + mem1.getUsername());
		mem1.setUsername("cos");
		System.out.println(TAG + "getter : " + mem1.getUsername());
		return "lombok Test 완료";
	}
}