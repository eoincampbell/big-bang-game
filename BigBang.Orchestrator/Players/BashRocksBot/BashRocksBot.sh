#!/bin/bash
HAND=(R P S L V)
RAND=`C:/Cygwin/bin/od -A n -t d -N 1 /dev/urandom | C:/Cygwin/bin/xargs`
C:/Cygwin/bin/echo ${HAND[ $RAND % 5 ]}