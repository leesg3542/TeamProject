select * from emp;

-- ���� ����Ŭ������ ��밡��

-- lower(ename) �ҹ��ڷ� ��ȯ
-- intcap(ename) ù�ڸ� �빮��
select ename, lower(ename), intcap(ename) from emp;

select * from emp;
select * from emp where ename = upper('smith');

select ename, concat(concat(ename, '('), empno), ')')
from emp;

select ename, ename || '(' || empno || ')' from emp;

select ename, substr(ename, 2, 3) from emp; -- 2~3�� �ڸ����� ���
--ename�� 3��° 

select ename from emp
where substr(ename, 3, 1) = 'A'; -- ename�� 3��° �ڸ��� A�� ���� ���� ���

-- like�� �ذ�
-- where ename like ' '�� ��밡���� Ư������ % : �ƹ��ų�, _ : �ݵ�� �ϳ�
select ename from emp
where ename like '_A%';
select ename, length(ename), length(sal) from emp;

-- ename�� �ڿ��� 3��°�� 'A'�� �� ename �˻�
select ename from emp
where substr(ename, -3, 1) = 'A';

-- ������ ���ڸ� ename�� -1�� ���� �� ������ ���ڸ� ���
select ename, substr(ename, -1)ename from emp;
-- ������ ���ڸ� ename�� ������ ������ ���ڷ� ���
select ename, substr(ename, length(ename), 1)ename from emp;

-- A�� ���° �ε����� �ִ��� ����ϴµ� ������ 0�� ���´�
select ename, instr(ename, 'A') from emp;
-- A�� ������ A���� 3�ڸ� ���ڸ� ������ ù��° �ڸ����� 3��° �ڸ� ���ڸ� ���
select ename, substr(ename, instr(ename, 'A'), 3) from emp;
-- ���� ���� �ڸ��� -_�� ������ ���� �ڸ��� _-�� 10�ڸ� ���ڰ� �� ������ ä���
select ename, lpad(ename, 10, '-_'), rpad(ename, 10, '_-') from emp;

-- ���� ���ڿ� S�� ������
select ename, ltrim(ename, 'S') from emp;

-- trim �����ʰ� ���� �� ������ ����
select ename from emp
where ename = trim('SMITH');

-- ����ڷκ��� �ؽ�Ʈ �ڽ��� �Է��� ���� ��� ������ �Է¹��� ���� trim�� ���Ѿ� �Ѵ�.
update emp set ename=trim(ename);
commit work

-- ename���� �˻� ����� A�� a�� ���̰� �Ѵ� ' '�ȿ� ���ڴ� ���ڿ��� �ƴ� �����̴�. B�� �����ϸ� B�� C�� �����ϸ� C��
-- ���� ������ �Ϸ��� replace�� ���
select ename, translate(ename, 'AB', 'ab') from emp;

-- 'A' ���ڿ��� 'aaa'�� �����Ѵ�. A�� �ִ� ���ڴ� aaa�� ����ȴ�
select ename, replace(ename, 'A', 'aaa') from emp;

-- round ù��° �Ҽ��� ���ڸ� ���ڸ� �ݿø�
-- trunc �Ҽ��� 1�ڸ� �� �� ����
select 234.567, round(234.567, 1), trunc(234.567, 1) from dual;

select sign(123), sign(0), sign(-123) from dual;
-- �ƽ�Ű�ڵ带 ���ڷ� ����
select chr(66) from dual;

-- ���� ��¥ ���
select sysdate from dual;

-- MONTHS_NETWEEN �� ��¥ ������ ������ ���
select sysdate, hiredate, trunc(MONTHS_BETWEEN(sysdate, hiredate),
from emp;

select sysdate+30, add_months(sysdate, 9) from emp;

-- 1�� �Ͽ����� �ǹ��Ѵ� ���� �Ͽ����� �����ΰ�
select sysdate, next_day(sysdate, 1) from dual;
-- �ش� 10���� ������ ���� �����ΰ�
select sysdate, last_day(sysdate-30) from dual;

-- to_number ���ڿ��� ���ڿ��� ����
select 123.5, to_number('123.45') from dual;

-- to_char ���ڿ��� ���ڿ��� ����
--000,000.0 .���� ���� �޸� ����, B�� 0�� 0�� �ƴ� �������� ���
-- $��ȣ�� ���� �տ� $ǥ��, $999
--L�� �ڿ� ȭ�� ��ȣ�� ���� �տ� Lǥ�� L999
-- .�� ����� ��ġ�� �Ҽ��� 999.99
-- ,�� ����� ��ġ�� �޸� 999,999
-- MI�� ������ ���̳ʽ� ��ȣ(���� ��) 999MI
-- PR�� ������ "()"�� ���´� 999PR
-- EEE �������� ��ȣ ǥ�� 99.999EEE
-- V 10�� n�� ���Ѵ� 9999V99 
-- ���� ����̶�� �Ѵ� �ϳ��ϳ� ������ ����ڰ� ���� �ؽ�Ʈ�� �Է��ؼ� ����ؾ� �ߴ� ����
select to_char(10234567.40, '9,999,999.9B') from dual;

-- ���� ��¥�� �ڵ����� ����ϴ� sysdate�� ���ڿ��� �����Ͽ� ���ϴ� ����� ��¥�������� ����
select sysdate, to_char(sysdate, 'YYYY-MM-DD hh24:mi:dd AM') from dual;
select sysdate, to_char(sysdate, 'YYYY-mon-DD hh24:mi:dd AM') from dual;


-- �߿� ����Ʈ�� �� ����
SELECT e.EMPNO, e.ENAME, e.DEPTNO, d.DNAME, e.SAL,
    DECODE ( (SELECT GRADE FROM SALGRADE s 
               WHERE e.SAL BETWEEN s.LOSAL AND s.HISAL)
           ,1,'1���' ,2,'2���', 3,'3���', 4, '4���', 5, '5���','9���') AS GRADE
  FROM EMP e, DEPT d
  WHERE e.DEPTNO = d.DEPTNO;


