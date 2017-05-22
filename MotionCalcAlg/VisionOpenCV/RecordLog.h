#include <memory>  
#include <ctime>  
#include <iostream>  
#include <fstream>  
class RecordLog  
{  
public:  
    static RecordLog * Inst(){  
        if (0 == _instance.get()){  
            _instance.reset(new RecordLog);  
        }  
        return _instance.get();  
    }  
  
    void Log(bool, std::string msg); // 写日志的方法  
private:  
    RecordLog(void){}  
    virtual ~RecordLog(void){}  
    friend class std::auto_ptr<RecordLog>;  
    static std::auto_ptr<RecordLog> _instance;  
};  
  
std::auto_ptr<RecordLog> RecordLog::_instance;  
  
void RecordLog::Log(bool valid,std::string loginfo) {
	string fileName = "valid.log";
	if(!valid)
	{
		fileName = "none.log";
	}
    std::ofstream ofs;  
    time_t t = time(0);
    char tmp[64];  
    strftime(tmp, sizeof(tmp), "\t[%Y.%m.%d %X %A]", localtime(&t));  
    ofs.open(fileName, std::ofstream::app);  
    ofs.write(loginfo.c_str(), loginfo.size());  
    ofs << tmp << '\n';  
    ofs.close();  
}  