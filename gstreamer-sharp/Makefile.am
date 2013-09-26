
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
	TagSetter.cs \
	Task.cs \
	TaskPool.cs \
	Utils.cs \
	Audio/StreamVolume.cs \
	Base/Adapter.cs \
	Base/BaseSink.cs \
	Base/BaseSrc.cs \
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
	GstSharp/TaskFunc.cs \
	Video/ColorBalance.cs \
	Video/Overlay.cs \
	GLib/Argv.cs \
	GLib/ConnectBeforeAttribute.cs \
	GLib/DefaultSignalHandlerAttribute.cs \
	GLib/DestroyNotify.cs \
	GLib/ExceptionManager.cs \
	GLib/FileUtils.cs \
	GLib/GException.cs \
	GLib/GInterfaceAdapter.cs \
	GLib/GInterfaceAttribute.cs \
	GLib/GLibSynchronizationContext.cs \
	GLib/Global.cs \
	GLib/GString.cs \
	GLib/GType.cs \
	GLib/GTypeAttribute.cs \
	GLib/Idle.cs \
	GLib/InitiallyUnowned.cs \
	GLib/IOChannel.cs \
	GLib/IWrapper.cs \
	GLib/KeyFile.cs \
	GLib/List.cs \
	GLib/ListBase.cs \
	GLib/Log.cs \
	GLib/MainContext.cs \
	GLib/MainLoop.cs \
	GLib/ManagedValue.cs \
	GLib/Markup.cs \
	GLib/Marshaller.cs \
	GLib/MissingIntPtrCtorException.cs \
	GLib/NotifyHandler.cs \
	GLib/Object.cs \
	GLib/ObjectManager.cs \
	GLib/Opaque.cs \
	GLib/ParamSpec.cs \
	GLib/Priority.cs \
	GLib/PropertyAttribute.cs \
	GLib/PtrArray.cs \
	GLib/Signal.cs \
	GLib/SignalArgs.cs \
	GLib/SignalAttribute.cs \
	GLib/SignalClosure.cs \
	GLib/SList.cs \
	GLib/Source.cs \
	GLib/Spawn.cs \
	GLib/Thread.cs \
	GLib/Timeout.cs \
	GLib/ToggleRef.cs \
	GLib/TypeFundamentals.cs \
	GLib/TypeInitializerAttribute.cs \
	GLib/Value.cs \
	GLib/ValueArray.cs \
	GLib/Variant.cs \
	GLib/VariantType.cs

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
	gcc -Wall -fPIC -c glue/*.c `pkg-config --cflags --libs gstreamer-base-1.0` \
	&& gcc -shared -Wl,-soname,libgstglue-1.0.so -o libgstglue-1.0.so *.o `pkg-config --cflags --libs gstreamer-base-1.0`

$(ASSEMBLY): $(FILES)
	$(CSC) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) -r:System.Core $(FILES)
