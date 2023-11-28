select * from emp;

-- 전부 오라클에서만 사용가능

-- lower(ename) 소문자로 변환
-- intcap(ename) 첫자만 대문자
select ename, lower(ename), intcap(ename) from emp;

select * from emp;
select * from emp where ename = upper('smith');

select ename, concat(concat(ename, '('), empno), ')')
from emp;

select ename, ename || '(' || empno || ')' from emp;

select ename, substr(ename, 2, 3) from emp; -- 2~3의 자리수만 출력
--ename의 3번째 

select ename from emp
where substr(ename, 3, 1) = 'A'; -- ename에 3번째 자리에 A가 들어가는 값을 출력

-- like로 해결
-- where ename like ' '에 사용가능한 특수문자 % : 아무거나, _ : 반드시 하나
select ename from emp
where ename like '_A%';
select ename, length(ename), length(sal) from emp;

-- ename의 뒤에서 3번째에 'A'가 들어간 ename 검색
select ename from emp
where substr(ename, -3, 1) = 'A';

-- 마지막 글자를 ename의 -1의 값을 빼 마지막 글자를 출력
select ename, substr(ename, -1)ename from emp;
-- 마지막 글자를 ename의 길이의 마지막 글자로 출력
select ename, substr(ename, length(ename), 1)ename from emp;

-- A가 몇번째 인덱스에 있는지 출력하는데 없으면 0이 나온다
select ename, instr(ename, 'A') from emp;
-- A가 있으면 A부터 3자리 숫자를 없으면 첫번째 자리에서 3번째 자리 숫자를 출력
select ename, substr(ename, instr(ename, 'A'), 3) from emp;
-- 왼쪽 문자 자리를 -_로 오른쪽 문자 자리를 _-로 10자리 숫자가 될 때까지 채운다
select ename, lpad(ename, 10, '-_'), rpad(ename, 10, '_-') from emp;

-- 끝에 글자에 S를 날린다
select ename, ltrim(ename, 'S') from emp;

-- trim 오른쪽과 왼쪽 끝 공백을 지움
select ename from emp
where ename = trim('SMITH');

-- 사용자로부터 텍스트 박스에 입력을 받을 경우 무조건 입력받은 값은 trim을 시켜야 한다.
update emp set ename=trim(ename);
commit work

-- ename에서 검색 결과에 A를 a로 보이게 한다 ' '안에 글자는 문자열이 아닌 문자이다. B른 선택하면 B를 C를 선택하면 C를
-- 전부 변경을 하려면 replace를 사용
select ename, translate(ename, 'AB', 'ab') from emp;

-- 'A' 문자열을 'aaa'로 변경한다. A가 있는 문자는 aaa로 변경된다
select ename, replace(ename, 'A', 'aaa') from emp;

-- round 첫번째 소숫점 한자리 숫자를 반올림
-- trunc 소숫점 1자리 수 외 절삭
select 234.567, round(234.567, 1), trunc(234.567, 1) from dual;

select sign(123), sign(0), sign(-123) from dual;
-- 아스키코드를 문자로 변경
select chr(66) from dual;

-- 오늘 날짜 출력
select sysdate from dual;

-- MONTHS_NETWEEN 두 날짜 사이의 월수를 계산
select sysdate, hiredate, trunc(MONTHS_BETWEEN(sysdate, hiredate),
from emp;

select sysdate+30, add_months(sysdate, 9) from emp;

-- 1은 일요일을 의미한다 다음 일요일은 언제인가
select sysdate, next_day(sysdate, 1) from dual;
-- 해당 10월의 마지막 일은 언제인가
select sysdate, last_day(sysdate-30) from dual;

-- to_number 문자열을 숫자열로 변경
select 123.5, to_number('123.45') from dual;

-- to_char 숫자열을 문자열로 변경
--000,000.0 .으로 숫자 콤마 선택, B는 0을 0이 아닌 공백으로 출력
-- $기호는 숫자 앞에 $표기, $999
--L운 자역 화패 기호로 숫자 앞에 L표기 L999
-- .은 명시한 위치에 소수점 999.99
-- ,은 명시한 위치에 콤마 999,999
-- MI는 우측에 마이너스 기호(음수 값) 999MI
-- PR은 음수를 "()"로 묶는다 999PR
-- EEE 과학전인 부호 표기 99.999EEE
-- V 10을 n번 곱한다 9999V99 
-- 옛날 방식이라고 한다 하나하나 일일히 사용자가 직접 텍스트로 입력해서 사용해야 했던 시절
select to_char(10234567.40, '9,999,999.9B') from dual;

-- 오늘 날짜를 자동으로 출력하는 sysdate를 문자열로 변경하여 원하는 방식의 날짜형식으로 변경
select sysdate, to_char(sysdate, 'YYYY-MM-DD hh24:mi:dd AM') from dual;
select sysdate, to_char(sysdate, 'YYYY-mon-DD hh24:mi:dd AM') from dual;


-- 중요 포인트인 것 같다
SELECT e.EMPNO, e.ENAME, e.DEPTNO, d.DNAME, e.SAL,
    DECODE ( (SELECT GRADE FROM SALGRADE s 
               WHERE e.SAL BETWEEN s.LOSAL AND s.HISAL)
           ,1,'1등급' ,2,'2등급', 3,'3등급', 4, '4등급', 5, '5등급','9등급') AS GRADE
  FROM EMP e, DEPT d
  WHERE e.DEPTNO = d.DEPTNO;


