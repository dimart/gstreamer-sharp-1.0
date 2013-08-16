
EXTRA_DIST =  

CSC = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize-
ASSEMBLY = bin/Debug/gstreamer-sharp.dll
TARGET = $(ASSEMBLY)
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug
gapidir = $(datadir)/gapi
gapi_DATA = $(APIS)

GSTREAMER_SHARP_DLL_MDB=

PROGRAMFILES = \
	$(GSTREAMER_SHARP_DLL_MDB)  

RESGEN=resgen2

install-data-local:
	@if test -n '$(TARGET)'; then                       \
	  echo "$(GACUTIL) /i $(ASSEMBLY) /f $(GACUTIL_FLAGS)";    \
	  $(GACUTIL) /i $(ASSEMBLY) /f $(GACUTIL_FLAGS) \
	  && echo "$(DESTDIR)$(libdir)" \
	  && cp libgstglue-1.0.so $(DESTDIR)$(libdir)|| exit 1; \
	fi

uninstall-local:
	@if test -n '$(TARGET)'; then                       \
	  echo "$(GACUTIL) /u $(ASSEMBLY_NAME) $(GACUTIL_FLAGS)";  \
	  $(GACUTIL) /u $(ASSEMBLY_NAME) $(GACUTIL_FLAGS) || exit 1;   \
	fi
	
all: $(ASSEMBLY) $(PROGRAMFILES) make-glue

FILES = \
	AssemblyInfo.cs \
	Application.cs \
	Manager.cs \
	Object.cs \
	Bus.cs \
	Element.cs \
	Message.cs \
	Structure.cs \
	MiniObject.cs \
	MessageHandler.cs \
	ElementFactory.cs \
	GstSharp/BusFunc.cs \
	Video/Overlay.cs \
	BasePlugins/PlaySink.cs \
	FakeObject.cs \
	Allocator.cs \
	Bin.cs \
	Buffer.cs \
	Memory.cs \
	State.cs \
	Pad.cs \
	Caps.cs \
	PadHandler.cs \
	Clock.cs \
	Audio/StreamVolume.cs \
	ChildProxy.cs \
	MapInfo.cs \
	BasePlugins/PlayBin.cs \
	Video/ColorBalance.cs \
	Pipeline.cs \
	CapsHandler.cs \
	Sample.cs \
	Query.cs \
	TagSetter.cs \
	Iterator.cs \
	Utils.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	GstSharp \
	Video \
	BasePlugins \
	Audio

REFERENCES =  \
	System \
	$(GLIB_SHARP_20_LIBS)

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) *.o libgstglue-1.0.so

distclean-local: 
	rm -rf obj


$(ASSEMBLY_MDB): $(ASSEMBLY) 

make-glue:
	gcc -Wall -fPIC -c glue/*.c `pkg-config --cflags --libs gstreamer-base-1.0` \
	&& gcc -shared -Wl,-soname,libgstglue-1.0.so -o libgstglue-1.0.so *.o `pkg-config --cflags --libs gstreamer-base-1.0`

$(ASSEMBLY): $(FILES)
	$(CSC) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) -r:System.Core -pkg:glib-sharp-2.0 $(FILES)
