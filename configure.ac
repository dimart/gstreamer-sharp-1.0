AC_INIT([gstreamer-sharp], [1.0.7], [inizan.yannick@gmail.com], [gstreamer-sharp])
AC_CONFIG_SRCDIR([Makefile.am])
AC_CONFIG_HEADERS(config.h)
AC_CONFIG_MACRO_DIR([m4])
AM_INIT_AUTOMAKE([check-news dist-bzip2])
m4_ifdef([AM_SILENT_RULES], [AM_SILENT_RULES([yes])])
AC_CONFIG_FILES([Makefile
		gstreamer-sharp/Makefile
		gstreamer-sharp-1.0.pc])

AM_PROG_AR
LT_INIT
AC_PROG_CC

API_VERSION=1.0.6
AC_SUBST(API_VERSION)
PACKAGE_VERSION=gstreamer-sharp-1.0
AC_SUBST(PACKAGE_VERSION)

MONO_REQUIRED_VERSION=2.4
AC_SUBST(MONO_REQUIRED_VERSION)
PKG_CHECK_MODULES(MONO_DEPENDENCY, mono >= $MONO_REQUIRED_VERSION, has_mono=true, has_mono=false)
PKG_CHECK_MODULES(GLIB_SHARP, glib-sharp-2.0 >= 2.12, have_glibsharp=yes, have_glibsharp=no)

GACUTIL='gacutil'

if test "x$has_mono" = "xtrue"; then
	GACUTIL_FLAGS='/package $(PACKAGE_VERSION) /gacdir $(libdir) /root $(DESTDIR)/$(libdir)'
	AC_PATH_PROG(MONO, mono, no)
	AC_PATH_PROG(CSC, gmcs, no)
	if test `uname -s` = "Darwin"; then
        	LIB_PREFIX=
	        LIB_SUFFIX=.dylib
	else
        	LIB_PREFIX=.so
	        LIB_SUFFIX=
	fi
else
	AC_PATH_PROG(CSC, csc.exe, no)
	GACUTIL_FLAGS=

	if test x$CSC = "xno"; then
        	AC_MSG_ERROR([You need to install either mono or .Net])
	else
		RUNTIME=
		LIB_PREFIX=
		LIB_SUFFIX=.dll
	fi
fi

GSTREAMER_REQUIRED_VERSION=1.0.6
PKG_CHECK_MODULES(GST,
	gstreamer-1.0 >= $GSTREAMER_REQUIRED_VERSION)
	
AC_SUBST(GACUTIL)
AC_SUBST(GACUTIL_FLAGS)
AC_SUBST(GST_LIBS)
AC_SUBST(GST_CFLAGS)

AC_OUTPUT
