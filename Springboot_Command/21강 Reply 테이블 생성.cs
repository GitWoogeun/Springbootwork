package com.cos.blog.model;

import java.sql.Timestamp;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;

import org.hibernate.annotations.CreationTimestamp;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Builder
@AllArgsConstructor
@NoArgsConstructor
@Data
@Entity
public class Reply {
		
    @Id							// Primary Key 제약조건 설정
    @GeneratedValue(strategy = GenerationType.IDENTITY)	// 프로젝트에서 연결된 DB의 넘버링 전략을 따라간다.
    private int id;		//	 시퀀스 	auto-increament 
    
    @Column(nullable = false, length = 200)
    private String content;		// 댓글 내용
    
    // 누가 어느 테이블에 댓글을 달것이냐 알려줘야 함 ( 연관 관계가 필요 )
    // 하나의 게시글에는 여러개의 답변을 받을 수 있다. ( 여러개가 앞으로 오고 To One )
    @ManyToOne
    @JoinColumn(name = "boardId")			// Board 테이블의 Id를 참조하고 있다,
    private Board board;

    // 이 답변을 누가 적었는지 알아야 하기 때문에
    // 하나의 유저는 여러개의 댓글을 달 수 있다.
    // @ManyToOne 어노테이션을 사용하려면 @JoinColumn 어노테이션을 사용해야 합니다.
    @ManyToOne
    @JoinColumn(name = "userId")				// 유저 테이블의 아이디를 참조
    private User user; 
    
    @CreationTimestamp      // 자동으로 댓글이 달린 시간으로 자동 저장
    private Timestamp createDate;  // 답변 등록 시간
}


