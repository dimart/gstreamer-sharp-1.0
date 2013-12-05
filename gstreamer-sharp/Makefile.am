
CSC = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize-
ASSEMBLY = gstreamer-sharp.dll
TARGET = $(ASSEMBLY)
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES = 
noinst_DATA = $(ASSEMBLY) $(ASSEMBLY).config
BUILD_DIR = bin/Debug
gapidir = $(datadir)/gapi
gapi_DATA = $(APIS)

EXTRA_DIST = $(ASSEMBLY).config.in

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
	Allocator.cs \
	Application.cs \
	Bin.cs \
	Buffer.cs \
	BufferList.cs \
	BufferPool.cs \
	Bus.cs \
	Caps.cs \
	CapsFeatures.cs \
	CapsHandler.cs \
	ChildProxy.cs \
	Clock.cs \
	Context.cs \
	Element.cs \
	ElementFactory.cs \
	Event.cs \
	Format.cs \
	Iterator.cs \
	Manager.cs \
	MapInfo.cs \
	Memory.cs \
	Message.cs \
	MessageHandler.cs \
	MiniObject.cs \
	Object.cs \
	Pad.cs \
	PadHandler.cs \
	Pipeline.cs \
	Query.cs \
	Sample.cs \
	Segment.cs \
	State.cs \
	Structure.cs \
	TagList.cs \
	TagSetter.cs \
	Task.cs \
	TaskPool.cs \
	Utils.cs \
	Audio/BaseSrc.cs \
	Audio/ChannelPosition.cs \
	Audio/Format.cs \
	Audio/FormatInfo.cs \
	Audio/Info.cs \
	Audio/RingBuffer.cs \
	Audio/RingBufferSpec.cs \
	Audio/Src.cs \
	Audio/StreamVolume.cs \
	Base/Adapter.cs \
	Base/Sink.cs \
	Base/Src.cs \
	Base/ByteReader.cs \
	Base/PushSrc.cs \
	BasePlugins/PlayBin.cs \
	BasePlugins/PlaySink.cs \
	CorePlugins/FdSink.cs \
	CorePlugins/FdSrc.cs \
	CorePlugins/FileSink.cs \
	CorePlugins/FileSrc.cs \
	CorePlugins/Queue.cs \
	CorePlugins/Tee.cs \
	GstSharp/BusFunc.cs \
	GstSharp/PackFunc.cs \
	GstSharp/TaskFunc.cs \
	Video/ColorBalance.cs \
	Video/Overlay.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	GstSharp \
	Video \
	BasePlugins \
	Audio

REFERENCES =  \
	System

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) *.o libgstglue-1.0.so $(ASSEMBLY)

distclean-local: 
	rm -rf obj


$(ASSEMBLY_MDB): $(ASSEMBLY) 

make-glue:
	gcc -Wall -fPIC -c glue/*.c `pkg-config --cflags --libs gstreamer-base-1.0 gstreamer-audio-1.0` \
	&& gcc -shared -Wl,-soname,libgstglue-1.0.so -o libgstglue-1.0.so *.o `pkg-config --cflags --libs gstreamer-audio-1.0 gstreamer-base-1.0`

$(ASSEMBLY): $(FILES)
	$(CSC) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) -r:System.Core -pkg:glib-sharp-3.0 $(FILES)
