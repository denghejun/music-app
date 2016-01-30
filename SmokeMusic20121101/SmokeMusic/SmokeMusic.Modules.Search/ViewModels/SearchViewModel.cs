using Microsoft.Practices.Prism.Commands;
using SmokeMusic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using SmokeMusic.Common.Events.Search;
using SmokeMusic.Common.Events.Channel;

namespace SmokeMusic.Modules.Search.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        #region 构造器
        public SearchViewModel()
        {
            this.SearchResultList = new ObservableCollection<Logic.Models.SearchResult>();
            if (!IsInDesignMode)
            {
                this.SearchBLL = this.UnityContainer.Resolve<Logic.Core.Search>();
                this.EventAggregator.GetEvent<ChangeChannelEvent>().Subscribe(this.OnChangeChannel);
            }
        }
        #endregion

        #region 属性
        private string _keywords;
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keywords
        {
            get { return _keywords; }
            set
            {
                if (_keywords != value)
                {
                    _keywords = value;
                    this.RaisePropertyChanged("Keywords");
                }
            }
        }
        /// <summary>
        /// 搜索结果集合
        /// </summary>
        public ObservableCollection<Logic.Models.SearchResult> SearchResultList { get; private set; }
        private Logic.Models.SearchResult _currentResult;
        /// <summary>
        /// 当前选中的结果
        /// 选中后就切换频道
        /// </summary>
        public Logic.Models.SearchResult CurrentResult
        {
            get { return _currentResult; }
            set
            {
                if (_currentResult != value)
                {
                    _currentResult = value;
                    this.RaisePropertyChanged("CurrentResult");
                    this.NotifyToChangeChannel();
                }
            }
        }
        /// <summary>
        /// 重写Title,用于注册Region
        /// </summary>
        public override string Title
        {
            get
            {
                return "Search";
            }
        }
        /// <summary>
        /// 重写KeepAlive,用于注册Region
        /// </summary>
        public override bool KeepAlive
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 搜索操作类
        /// </summary>
        Logic.Core.Search SearchBLL { get; set; }
        #endregion

        #region 命令
        private DelegateCommand _searchCommand;
        /// <summary>
        /// 搜索命令
        /// </summary>
        public DelegateCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new DelegateCommand(this.Search);
                }
                return _searchCommand;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 搜索频道
        /// </summary>
        public void Search()
        {
            this.IsLoading = true;
            var action = new Func<string, int, List<Logic.Models.SearchResult>>(this.SearchBLL.RemoteSearch);
            action.BeginInvoke(this.Keywords, 1, ar => 
            {
                var list = action.EndInvoke(ar);
                this.InvokeOnUIDispatcher(new Action(() =>
                {
                    foreach (var item in list)
                    {
                        this.SearchResultList.Add(item);
                    }
                    this.IsLoading = false;
                }));
            }, null);
        }
        /// <summary>
        /// 通知更换频道
        /// </summary>
        public void NotifyToChangeChannel()
        {
            if (this.CurrentResult == null) return;
            this.EventAggregator.GetEvent<ChooseSearchResultEvent>().Publish(this.CurrentResult);
        }
        /// <summary>
        /// 在更换频道时触发的方法
        /// </summary>
        /// <param name="channel"></param>
        public void OnChangeChannel(Logic.Models.Channel channel)
        {
            //如果更换的频道不是搜索到的频道,则取消所有选中
            if (channel == null) return;
            if (this.CurrentResult == null) return;
            if (channel.Context != this.CurrentResult.Context)
            {
                this.CurrentResult = null;
            }
        }
        #endregion
    }
}
