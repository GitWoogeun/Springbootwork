package com.cos.blog.model;

import java.sql.Timestamp;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import org.hibernate.annotations.CreationTimestamp;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

// ORM -> Java Object를 테이블로 매핑해주는 기술
// 이 클래스를 테이블화 시키기 위해서는 Entity라는 어노테이션을 쓴다.
// User클래스가 변수들을 읽어서 자동으로 MySQL에 테이블이 생성이 된다.
@Data
@NoArgsConstructor   // 빈 생성자
@AllArgsConstructor   // 전체 생성자
@Builder						   // 빌더 패턴!! ( 나중에 알아서 알게 된다 )
//@DynamicInsert			  // insert할때 null인 필드를 제외 시켜준다. ( role 같은 경우 Default 데이터를 받을려면 DynamicInsert 어노테이션을 사용 할 수도 있다. )
@Entity							  // @Entity는 ORM 클래스다 라고 말해주는거기 때문에 제일 가까이 써주는게 좋다
public class User {
	
		@Id		// Primary Key																					// Oracle일 경우 시퀀스, MySQL일 경우 auto_increament를 따라감
		@GeneratedValue(strategy = GenerationType.IDENTITY)			//(GenerationType.IDENTITY) 프로젝트에서 연결된 DB의 넘버링 전략을 따라간다.
		private int id;																								// Oracle == 시퀀스, MySQL == auto_increament
		
		// null값이면 안된다. / 아이디의 길이는 30자까지 / 중복된 값을 넣어주지 않는다.
		@Column(nullable = false, length = 100, unique = true)
		private String username;					// 아이디
		
		// null값이면 안된다. / 아이디의 길이는 100자까지  => 해쉬 ( 비밀번호 암호화 할꺼임 )
		@Column(nullable = false, length = 100)
		private String password;					// 비밀번호
		
		@Column(nullable = false, length = 50)
		private String email;							// 이메일
		
//    Enum방식으로 사용
//		// (" 'user' ") 문자열이라는걸 알려줘야한다.
//		@ColumnDefault("'user'")							// Enum을 사용하면 도메인을 설정할수있다. ( 도메인이 정해졌다 == 어떤 범위가 정해 졌다.  EX 성별이라치면 남, 녀로만 )
//		private String role;										// Enum을 쓰는게 좋다. ( Role : 어떤 회원가입을 했을 때 이 사람은 admin, user, manage )
											
		
		// DB는 RoleType 이라는게 없다. 그렇기 때문에 @Enumerated ( EnumType.STRING )으로 문자열이라고 명시 해줘야한다.
		@Enumerated(EnumType.STRING)
		private RoleType role; 	// Enum 방식 사용	// ADMIN, USER
		
		// 회원의 가입의 시간										// Spring에서 @CreationTimestamp 어노테이션을 붙이면 => 자바에서 현재시간을 만들어서 insert해준다.
		@CreationTimestamp								// 시간이 자동으로 입력
		private Timestamp	createDate;				// 등록일자 ( // Java SQL이 가지고 있음 )
}