package com.cos.blog.model;

import java.sql.Timestamp;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.CreationTimestamp;

// ORM -> Java Object를 테이블로 매핑해주는 기술
// 이 클래스를 테이블화 시키기 위해서는 Entity라는 어노테이션을 쓴다.
// User클래스가 변수들을 읽어서 자동으로 MySQL에 테이블이 생성이 된다.
@Entity
public class User {
	
		@Id		// Primary Key																					// Oracle일 경우 시퀀스, MySQL일 경우 auto_increament를 따라감
		@GeneratedValue(strategy = GenerationType.IDENTITY)			//(GenerationType.IDENTITY) 프로젝트에서 연결된 DB의 넘버링 전략을 따라간다.
		private int id;																								// Oracle == 시퀀스, MySQL == auto_increament
		
		// null값이면 안된다. / 아이디의 길이는 30자까지
		@Column(nullable = false, length = 30)
		private String username;					// 아이디
		
		// null값이면 안된다. / 아이디의 길이는 100자까지  => 해쉬 ( 비밀번호 암호화 할꺼임 )
		@Column(nullable = false, length = 100)
		private String password;					// 비밀번호
		
		@Column(nullable = false, length = 50)
		private String email;							// 이메일
		
		// (" 'user' ") 문자열이라는걸 알려줘야한다.
		@ColumnDefault("'user'")					// Enum을 사용하면 도메인을 설정할수있다. ( 도메인이 정해졌다 == 어떤 범위가 정해 졌다.  EX 성별이라치면 남, 녀로만 )
		private String role;								// Enum을 쓰는게 좋다. ( Role : 어떤 회원가입을 했을 때 이 사람은 admin, user, manage )
		
		// 회원의 가입의 시간
		@CreationTimestamp						// 시간이 자동으로 입력
		private Timestamp	createDate;		// 등록일자 ( // Java SQL이 가지고 있음 )
		
}