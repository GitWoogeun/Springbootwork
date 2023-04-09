package com.cos.blog.model;

import java.sql.Timestamp;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.Lob;
import javax.persistence.ManyToOne;

import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.CreationTimestamp;

@Entity
public class Board {
	
		@Id				// Primary Key
		@GeneratedValue(strategy = GenerationType.IDENTITY)		// auto_increament
		private int id;
		
		@Column(nullable = false, length = 100)
		private String title;
		
		@Lob   // 대용량 데이터를 담을 때 쓰는 어노테이션
		private String content;		// 섬머노트 라이브러리 ( 일반 글이 디자인이되어서 들어가는데 html 태그가 섞여서 들어간다 그래서 글자의 용량이 커진다 )
		
		@ColumnDefault("0")			// 얘는 int니까 ' '가 필요 없다.
		private int count; 				// 조회수
		
		@ManyToOne   					// Many = Board / One = User : 한명의 유저는 여러개의 게시글을 쓸수 있다.
		@JoinColumn(name = "userId")		// 데이터베이스의 컬럼으로는 userId로 만들어질거에요~
		private User user; 				// DB는 오브젝트를 저장할 수 없다. FK, 자바는 오브젝트를 저장할 수 있다.
														// 자바에서는 Object를 저장할 수 있다, 데이터베이스에는 오브젝트를 저장할 수 없다.
														// 그렇기 때문에 자바가 데이터베이스에 맞춰서 JPA를 사용하면 오브젝트를 저장할 수 있다. ( FK가 된다 )
		
		@CreationTimestamp		// 데이터가 insert, update일시 현재시간이 들어감
		private Timestamp createDate;	
}