﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UsersDiosna.DBML
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="MainDB")]
	public partial class CMSDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSection(Section instance);
    partial void UpdateSection(Section instance);
    partial void DeleteSection(Section instance);
    partial void InsertArticle(Article instance);
    partial void UpdateArticle(Article instance);
    partial void DeleteArticle(Article instance);
    #endregion
		
		public CMSDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["MainDBConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CMSDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CMSDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CMSDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CMSDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Section> Sections
		{
			get
			{
				return this.GetTable<Section>();
			}
		}
		
		public System.Data.Linq.Table<Article> Articles
		{
			get
			{
				return this.GetTable<Article>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Sections")]
	public partial class Section : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
		private string _Name;
		
		private string _Description;
		
		private System.Nullable<long> _ArticleId;
		
		private string _Role;
		
		private System.Nullable<int> _BakeryId;
		
		private EntitySet<Article> _Articles;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnArticleIdChanging(System.Nullable<long> value);
    partial void OnArticleIdChanged();
    partial void OnRoleChanging(string value);
    partial void OnRoleChanged();
    partial void OnBakeryIdChanging(System.Nullable<int> value);
    partial void OnBakeryIdChanged();
    #endregion
		
		public Section()
		{
			this._Articles = new EntitySet<Article>(new Action<Article>(this.attach_Articles), new Action<Article>(this.detach_Articles));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(MAX)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(MAX)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ArticleId", DbType="BigInt")]
		public System.Nullable<long> ArticleId
		{
			get
			{
				return this._ArticleId;
			}
			set
			{
				if ((this._ArticleId != value))
				{
					this.OnArticleIdChanging(value);
					this.SendPropertyChanging();
					this._ArticleId = value;
					this.SendPropertyChanged("ArticleId");
					this.OnArticleIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Role", DbType="NVarChar(MAX)")]
		public string Role
		{
			get
			{
				return this._Role;
			}
			set
			{
				if ((this._Role != value))
				{
					this.OnRoleChanging(value);
					this.SendPropertyChanging();
					this._Role = value;
					this.SendPropertyChanged("Role");
					this.OnRoleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BakeryId", DbType="Int")]
		public System.Nullable<int> BakeryId
		{
			get
			{
				return this._BakeryId;
			}
			set
			{
				if ((this._BakeryId != value))
				{
					this.OnBakeryIdChanging(value);
					this.SendPropertyChanging();
					this._BakeryId = value;
					this.SendPropertyChanged("BakeryId");
					this.OnBakeryIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Section_Article", Storage="_Articles", ThisKey="Id", OtherKey="SectionId")]
		public EntitySet<Article> Articles
		{
			get
			{
				return this._Articles;
			}
			set
			{
				this._Articles.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Articles(Article entity)
		{
			this.SendPropertyChanging();
			entity.Section = this;
		}
		
		private void detach_Articles(Article entity)
		{
			this.SendPropertyChanging();
			entity.Section = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Articles")]
	public partial class Article : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _Id;
		
		private int _bakeryId;
		
		private System.Nullable<System.DateTime> _DateTime;
		
		private string _Author;
		
		private string _Header;
		
		private string _Text;
		
		private string _Amount;
		
		private System.Nullable<int> _HoursSpend;
		
		private string _Attachment;
		
		private string _Description;
		
		private System.Nullable<long> _SectionId;
		
		private System.DateTime _DateTimeOrigin;
		
		private EntityRef<Section> _Section;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(long value);
    partial void OnIdChanged();
    partial void OnbakeryIdChanging(int value);
    partial void OnbakeryIdChanged();
    partial void OnDateTimeChanging(System.Nullable<System.DateTime> value);
    partial void OnDateTimeChanged();
    partial void OnAuthorChanging(string value);
    partial void OnAuthorChanged();
    partial void OnHeaderChanging(string value);
    partial void OnHeaderChanged();
    partial void OnTextChanging(string value);
    partial void OnTextChanged();
    partial void OnAmountChanging(string value);
    partial void OnAmountChanged();
    partial void OnHoursSpendChanging(System.Nullable<int> value);
    partial void OnHoursSpendChanged();
    partial void OnAttachmentChanging(string value);
    partial void OnAttachmentChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnSectionIdChanging(System.Nullable<long> value);
    partial void OnSectionIdChanged();
    partial void OnDateTimeOriginChanging(System.DateTime value);
    partial void OnDateTimeOriginChanged();
    #endregion
		
		public Article()
		{
			this._Section = default(EntityRef<Section>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_bakeryId", DbType="Int NOT NULL")]
		public int bakeryId
		{
			get
			{
				return this._bakeryId;
			}
			set
			{
				if ((this._bakeryId != value))
				{
					this.OnbakeryIdChanging(value);
					this.SendPropertyChanging();
					this._bakeryId = value;
					this.SendPropertyChanged("bakeryId");
					this.OnbakeryIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateTime", DbType="DateTime")]
		public System.Nullable<System.DateTime> DateTime
		{
			get
			{
				return this._DateTime;
			}
			set
			{
				if ((this._DateTime != value))
				{
					this.OnDateTimeChanging(value);
					this.SendPropertyChanging();
					this._DateTime = value;
					this.SendPropertyChanged("DateTime");
					this.OnDateTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Author", DbType="NVarChar(50)")]
		public string Author
		{
			get
			{
				return this._Author;
			}
			set
			{
				if ((this._Author != value))
				{
					this.OnAuthorChanging(value);
					this.SendPropertyChanging();
					this._Author = value;
					this.SendPropertyChanged("Author");
					this.OnAuthorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Header", DbType="NVarChar(150)")]
		public string Header
		{
			get
			{
				return this._Header;
			}
			set
			{
				if ((this._Header != value))
				{
					this.OnHeaderChanging(value);
					this.SendPropertyChanging();
					this._Header = value;
					this.SendPropertyChanged("Header");
					this.OnHeaderChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Text", DbType="NVarChar(MAX)")]
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				if ((this._Text != value))
				{
					this.OnTextChanging(value);
					this.SendPropertyChanging();
					this._Text = value;
					this.SendPropertyChanged("Text");
					this.OnTextChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Amount", DbType="NChar(10)")]
		public string Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if ((this._Amount != value))
				{
					this.OnAmountChanging(value);
					this.SendPropertyChanging();
					this._Amount = value;
					this.SendPropertyChanged("Amount");
					this.OnAmountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HoursSpend", DbType="Int")]
		public System.Nullable<int> HoursSpend
		{
			get
			{
				return this._HoursSpend;
			}
			set
			{
				if ((this._HoursSpend != value))
				{
					this.OnHoursSpendChanging(value);
					this.SendPropertyChanging();
					this._HoursSpend = value;
					this.SendPropertyChanged("HoursSpend");
					this.OnHoursSpendChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Attachment", DbType="VarChar(MAX)")]
		public string Attachment
		{
			get
			{
				return this._Attachment;
			}
			set
			{
				if ((this._Attachment != value))
				{
					this.OnAttachmentChanging(value);
					this.SendPropertyChanging();
					this._Attachment = value;
					this.SendPropertyChanged("Attachment");
					this.OnAttachmentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NChar(10)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SectionId", DbType="BigInt")]
		public System.Nullable<long> SectionId
		{
			get
			{
				return this._SectionId;
			}
			set
			{
				if ((this._SectionId != value))
				{
					if (this._Section.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnSectionIdChanging(value);
					this.SendPropertyChanging();
					this._SectionId = value;
					this.SendPropertyChanged("SectionId");
					this.OnSectionIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateTimeOrigin", AutoSync=AutoSync.Always)]
		public System.DateTime DateTimeOrigin
		{
			get
			{
				return this._DateTimeOrigin;
			}
			set
			{
				if ((this._DateTimeOrigin != value))
				{
					this.OnDateTimeOriginChanging(value);
					this.SendPropertyChanging();
					this._DateTimeOrigin = value;
					this.SendPropertyChanged("DateTimeOrigin");
					this.OnDateTimeOriginChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Section_Article", Storage="_Section", ThisKey="SectionId", OtherKey="Id", IsForeignKey=true)]
		public Section Section
		{
			get
			{
				return this._Section.Entity;
			}
			set
			{
				Section previousValue = this._Section.Entity;
				if (((previousValue != value) 
							|| (this._Section.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Section.Entity = null;
						previousValue.Articles.Remove(this);
					}
					this._Section.Entity = value;
					if ((value != null))
					{
						value.Articles.Add(this);
						this._SectionId = value.Id;
					}
					else
					{
						this._SectionId = default(Nullable<long>);
					}
					this.SendPropertyChanged("Section");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
