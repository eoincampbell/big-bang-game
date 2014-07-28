#!/bin/bash
HAND=(R P S L V)
RAND=`/cygdrive/c/cygwin/bin/od -A n -t d -N 1 /dev/urandom | /cygdrive/c/cygwin/bin/xargs`
/cygdrive/c/cygwin/bin/echo ${HAND[ $RAND  % 5 ]}
