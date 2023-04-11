package com.cos.blog.handler;

import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestController;

import com.cos.blog.dto.ResponseDto;

//모든 Exception이 발생하면 GlobalExceptionHandler 클래스로 들어온다.
@ControllerAdvice   
@RestController
public class GlobalExceptionHandler {
	
		// Spring이 모든 Exception을 받아서 GlobalExceptionHandler 클래스에 보내준다.
		@ExceptionHandler(value = Exception.class)
		public ResponseDto<String> handleArgumentException( Exception e ) {
			return new ResponseDto<String>(HttpStatus.OK.value(), e.getMessage());
		}
}
