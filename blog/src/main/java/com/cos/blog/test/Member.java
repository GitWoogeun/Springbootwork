package com.cos.blog.test;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

// 접근제어자
// private은   : 다이렉트로 접근하지마!
// public은      : 어디서든 접근할 수 있어~
// protected : 내 자식들에게만 상속해줄거야~
@Data										// getter와 setter
@NoArgsConstructor			// 빈 생성자
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
	
//	// 나중에 Id값을 시퀀스로 돌릴려면 사용자 생성자가 한개 더 Overriding이 되어 있어야 한다.
//	public Member(String username, String password, String email) {
//		this.username = username;
//		this.password = password;
//		this.email = email;
//	}
}
