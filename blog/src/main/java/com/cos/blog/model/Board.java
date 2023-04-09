package com.cos.blog.model;

import java.sql.Timestamp;
import java.util.List;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.Lob;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;

import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.CreationTimestamp;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;


@Data								// 자동으로 Getter, Setter 생성
@NoArgsConstructor   // 빈 생성자
@AllArgsConstructor   // 전체 생성자
@Builder						   // 빌더 패턴!! ( 나중에 알아서 알게 된다 )
@Entity							  // @Entity는 ORM 클래스다 라고 말해주는거기 때문에 제일 가까이 써주는게 좋다
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
		
		// Board라는 테이블에 userId 컬럼은 User테이블의  Id라는 컬럼을 참조하고 있다고 자동으로 FK 제약조건 생성
		@ManyToOne (fetch = FetchType.EAGER)  		// Many = Board / One = User : 한명의 유저는 여러개의 게시글을 쓸수 있다.
		@JoinColumn(name = "userId")		// 데이터베이스의 컬럼으로는 userId로 만들어질거에요~
		private User user; 				// DB는 오브젝트를 저장할 수 없다. FK, 자바는 오브젝트를 저장할 수 있다.
														// 자바에서는 Object를 저장할 수 있다, 데이터베이스에는 오브젝트를 저장할 수 없다.
														// 그렇기 때문에 자바가 데이터베이스에 맞춰서 JPA를 사용하면 오브젝트를 저장할 수 있다. ( FK가 된다 )
	
		/*
		 * 	# @ManyToOne (fetch = FetchType.EAGER)
    			=> 한마디로 너가 board 테이블을 select하면 user테이블은 Join해서 가져올게 한건 밖에 없으니까.

			    # @OneToMany (fetch = FetchType.LAZY)
			    => 필요하면 들고오고 필요하지 않으면 안들고 올게
		 * */
		
		//		@JoinColumn(name = "replyId")		// 이건 필요가 없다 : 실제 Board테이블에 Database에 FK replayId가 필요가 없다
		//		: 1 정규화가 깨짐 ( 하나의 컬럼은 원자성을 가진다 하나의 값을 가져야 한다. )
		//하나의 게시글은 여러개의 댓글을 가질수 있기 때문에 OneToMany
		@OneToMany(mappedBy = "board", fetch = FetchType.EAGER)		// mapped By 연관관계의 주인이 아니다 ( 난 FK가 아니에요~ ) DB에 컬럼을 만들지 마세요!
		private List<Reply> replay;						// mapped By의 board는 Reply 클래스에 있는 Board 타입의 board 변수
		// 나는 그냥 Board를 Select할때 Join문을 통해서 값을 얻기위해 필요한 겁니다.
		
		@CreationTimestamp		// 데이터가 insert, update일시 현재시간이 들어감
		private Timestamp createDate;	
}