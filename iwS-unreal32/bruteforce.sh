#!/bin/sh

OFFSET=-5000

until [ "$OFFSET" -gt "5000" ]; do
	./iwS-unreal $OFFSET
	echo "offset: $OFFSET" > file
	OFFSET=$[$OFFSET+5]
done
