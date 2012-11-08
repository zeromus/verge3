
namespace winmaped2
{
	static class BiosFont
	{

		const int xx = 1;
		const int zz = 0;
		static readonly byte[][] smal_tbl;
		static BiosFont()
		{
			smal_tbl = new byte[][]
			{ sBlank,
				sYow,  sQuote,    sNum,   sBuck,sPercnt, sCarot, sQuotes, slParen,
			 srParen,    star,    sPtr,  sComma, sMinus,sPeriod,  sSlash,      s0,
				  s1,      s2,      s3,      s4,     s5,     s6,      s7,      s8,
				  s9,  sColon,  ssemic,      ss,    sEq,    sra,  sQuest,     sAT,
				 sbA,     sbB,     sbC,     sbD,    sbE,    sbF,     sbG,     sbH,
				 sbI,     sbJ,     sbK,     sbL,    sbM,    sbN,     sbO,     sbP,
				 sbQ,     sbR,     sbS,     sbT,    sbU,    sbV,     sbW,     sbX,
				 sbY,     sbZ,      ss, sbSlash,     ss, sCarot,     usc,     sch,
				 ssA,     ssB,     ssC,     ssD,    ssE,    ssF,     ssG,     ssH,
				 ssI,     ssJ,     ssK,     ssL,    ssM,    ssN,     ssO,     ssP,
				 ssQ,     ssR,     ssS,     ssT,    ssU,    ssV,     ssW,     ssX,
				 ssY,     ssZ,      ss,  target,  check,  sCopy,  sBlock,     ss};
		}


		public static byte[] Glyph(char c)
		{
			return smal_tbl[c - ' '];
		}


		static readonly byte[] sbA = new byte[]
		   {4,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,xx,xx,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx};

		static readonly byte[] ssA = new byte[]
		   {5,
			zz,zz,zz,zz,zz,
			zz,zz,zz,zz,zz,
			zz,xx,xx,zz,zz,
			xx,zz,zz,xx,zz,
			xx,zz,zz,xx,zz,
			xx,zz,zz,xx,zz,
			zz,xx,xx,zz,xx};

		static readonly byte[] sbB = new byte[]
		   {4,
			xx,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,xx,xx,zz};

		static readonly byte[] ssB = new byte[]
		   {4,
			xx,zz,zz,zz,
			xx,zz,zz,zz,
			xx,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,xx,xx,zz};

		static readonly byte[] sbC = new byte[]
		   {4,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,zz,
			xx,zz,zz,zz,
			xx,zz,zz,zz,
			xx,zz,zz,xx,
			zz,xx,xx,zz};

		static readonly byte[] ssC = new byte[]
		   {3,
			zz,zz,zz,
			zz,zz,zz,
			zz,xx,xx,
			xx,zz,zz,
			xx,zz,zz,
			xx,zz,zz,
			zz,xx,xx};

		static readonly byte[] sbD = new byte[]
		   {4,
			xx,xx,zz,zz,
			xx,zz,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,xx,zz,
			xx,xx,zz,zz};


		static readonly byte[] ssD = new byte[]
		   {4,
			zz,zz,zz,xx,
			zz,zz,zz,xx,
			zz,xx,xx,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			zz,xx,xx,xx};

		static readonly byte[] sbE = new byte[]
		   {4,
			xx,xx,xx,xx,
			xx,zz,zz,zz,
			xx,zz,zz,zz,
			xx,xx,xx,zz,
			xx,zz,zz,zz,
			xx,zz,zz,zz,
			xx,xx,xx,xx};

		static readonly byte[] ssE = new byte[]
		   {4,
			zz,zz,zz,zz,
			zz,zz,zz,zz,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			xx,xx,xx,zz,
			xx,zz,zz,zz,
			zz,xx,xx,zz};

		static readonly byte[] sbF = new byte[]
		   {4,
			xx,xx,xx,xx,
			xx,zz,zz,zz,
			xx,zz,zz,zz,
			xx,xx,xx,zz,
			xx,zz,zz,zz,
			xx,zz,zz,zz,
			xx,zz,zz,zz};

		static readonly byte[] ssF = new byte[]
		   {4,
			zz,zz,xx,zz,
			zz,xx,zz,xx,
			zz,xx,zz,zz,
			xx,xx,xx,zz,
			zz,xx,zz,zz,
			zz,xx,zz,zz,
			zz,xx,zz,zz};

		static readonly byte[] sbG = new byte[]
		   {4,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,zz,
			xx,zz,zz,zz,
			xx,zz,xx,xx,
			xx,zz,zz,xx,
			zz,xx,xx,zz};

		static readonly byte[] ssG = new byte[]
		   {4,
			zz,zz,zz,zz,
			zz,zz,zz,zz,
			zz,xx,xx,zz,
			xx,zz,zz,zz,
			xx,zz,xx,xx,
			xx,zz,zz,xx,
			zz,xx,xx,zz};


		static readonly byte[] sbH = new byte[]
		   {4,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,xx,xx,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx};

		static readonly byte[] ssH = new byte[]
		   {4,
			xx,zz,zz,zz,
			xx,zz,zz,zz,
			xx,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx};

		static readonly byte[] sbI = new byte[]
		   {3,
			xx,xx,xx,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz,
			xx,xx,xx};

		static readonly byte[] ssI = new byte[]
		   {1,
			zz,
			xx,
			zz,
			xx,
			xx,
			xx,
			xx};

		static readonly byte[] sbJ = new byte[]
		   {4,
			zz,zz,zz,xx,
			zz,zz,zz,xx,
			zz,zz,zz,xx,
			zz,zz,zz,xx,
			zz,zz,zz,xx,
			xx,zz,zz,xx,
			zz,xx,xx,zz};

		static readonly byte[] ssJ = new byte[]
		   {3,
			zz,zz,xx,
			zz,zz,zz,
			zz,zz,xx,
			zz,zz,xx,
			zz,zz,xx,
			xx,zz,xx,
			zz,xx,zz};

		static readonly byte[] sbK = new byte[]
		   {4,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,xx,zz,
			xx,xx,zz,zz,
			xx,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx};

		static readonly byte[] ssK = new byte[]
		   {3,
			xx,zz,zz,
			xx,zz,zz,
			xx,zz,xx,
			xx,zz,xx,
			xx,xx,zz,
			xx,zz,xx,
			xx,zz,xx};

		static readonly byte[] sbL = new byte[]
		   {3,
			xx,zz,zz,
			xx,zz,zz,
			xx,zz,zz,
			xx,zz,zz,
			xx,zz,zz,
			xx,zz,zz,
			xx,xx,xx};

		static readonly byte[] ssL = new byte[]
		   {3,
			zz,zz,zz,
			zz,zz,zz,
			xx,zz,zz,
			xx,zz,zz,
			xx,zz,zz,
			xx,zz,zz,
			xx,xx,xx};

		static readonly byte[] sbM = new byte[]
		   {5,
			xx,zz,zz,zz,xx,
			xx,xx,zz,xx,xx,
			xx,zz,xx,zz,xx,
			xx,zz,xx,zz,xx,
			xx,zz,zz,zz,xx,
			xx,zz,zz,zz,xx,
			xx,zz,zz,zz,xx};

		static readonly byte[] ssM = new byte[]
		   {5,
			zz,zz,zz,zz,zz,
			zz,zz,zz,zz,zz,
			zz,xx,zz,xx,zz,
			xx,zz,xx,zz,xx,
			xx,zz,xx,zz,xx,
			xx,zz,xx,zz,xx,
			xx,zz,xx,zz,xx};

		static readonly byte[] sbN = new byte[]
		   {4,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,xx,zz,xx,
			xx,zz,xx,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx};

		static readonly byte[] ssN = new byte[]
		   {4,
			zz,zz,zz,zz,
			zz,zz,zz,zz,
			xx,zz,xx,zz,
			xx,xx,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx};

		static readonly byte[] sbO = new byte[]
		   {4,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			zz,xx,xx,zz};

		static readonly byte[] ssO = new byte[]
		   {4,
			zz,zz,zz,zz,
			zz,zz,zz,zz,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			zz,xx,xx,zz};

		static readonly byte[] sbP = new byte[]
		   {4,
			xx,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,xx,xx,zz,
			xx,zz,zz,zz,
			xx,zz,zz,zz,
			xx,zz,zz,zz};

		static readonly byte[] ssP = new byte[]
		   {4,
			zz,zz,zz,zz,
			zz,zz,zz,zz,
			xx,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,xx,xx,zz,
			xx,zz,zz,zz};

		static readonly byte[] sbQ = new byte[]
		   {4,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,xx,zz,
			zz,xx,zz,xx};

		static readonly byte[] ssQ = new byte[]
		   {4,
			zz,zz,zz,zz,
			zz,zz,zz,zz,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,xx,zz,
			zz,xx,zz,xx};

		static readonly byte[] sbR = new byte[]
		   {4,
			xx,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			xx,zz,zz,xx};

		static readonly byte[] ssR = new byte[]
		   {4,
			zz,zz,zz,zz,
			zz,zz,zz,zz,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,zz,
			xx,zz,zz,zz,
			xx,zz,zz,zz};

		static readonly byte[] sbS = new byte[]
		   {4,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,zz,
			zz,xx,xx,zz,
			zz,zz,zz,xx,
			xx,zz,zz,xx,
			zz,xx,xx,zz,};

		static readonly byte[] ssS = new byte[]
		   {3,
			zz,zz,zz,
			zz,zz,zz,
			zz,xx,xx,
			xx,zz,zz,
			zz,xx,zz,
			zz,zz,xx,
			xx,xx,zz};

		static readonly byte[] sbT = new byte[]
		   {3,
			xx,xx,xx,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz};

		static readonly byte[] ssT = new byte[]
		   {3,
			zz,xx,zz,
			zz,xx,zz,
			xx,xx,xx,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,xx};

		static readonly byte[] sbU = new byte[]
		   {3,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,xx,xx};

		static readonly byte[] ssU = new byte[]
		   {3,
			zz,zz,zz,
			zz,zz,zz,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,xx,xx};

		static readonly byte[] sbV = new byte[]
		   {3,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			zz,xx,zz};

		static readonly byte[] ssV = new byte[]
		   {3,
			zz,zz,zz,
			zz,zz,zz,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			zz,xx,zz};

		static readonly byte[] sbW = new byte[]
		   {5,
			xx,zz,zz,zz,xx,
			xx,zz,zz,zz,xx,
			xx,zz,zz,zz,xx,
			xx,zz,xx,zz,xx,
			xx,zz,xx,zz,xx,
			xx,xx,zz,xx,xx,
			xx,zz,zz,zz,xx};

		static readonly byte[] ssW = new byte[]
		   {5,
			zz,zz,zz,zz,zz,
			zz,zz,zz,zz,zz,
			xx,zz,zz,zz,xx,
			xx,zz,xx,zz,xx,
			xx,zz,xx,zz,xx,
			xx,xx,zz,xx,xx,
			xx,zz,zz,zz,xx};

		static readonly byte[] sbX = new byte[]
		   {5,
			xx,zz,zz,zz,xx,
			xx,zz,zz,zz,xx,
			zz,xx,zz,xx,zz,
			zz,zz,xx,zz,zz,
			zz,xx,zz,xx,zz,
			xx,zz,zz,zz,xx,
			xx,zz,zz,zz,xx};

		static readonly byte[] ssX = new byte[]
		   {3,
			zz,zz,zz,
			zz,zz,zz,
			xx,zz,xx,
			xx,zz,xx,
			zz,xx,zz,
			xx,zz,xx,
			xx,zz,xx};

		static readonly byte[] sbY = new byte[]
		   {3,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz};

		static readonly byte[] ssY = new byte[]
		   {3,
			zz,zz,zz,
			zz,zz,zz,
			xx,zz,xx,
			xx,zz,xx,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz};

		static readonly byte[] sbZ = new byte[]
		   {5,
			xx,xx,xx,xx,xx,
			zz,zz,zz,zz,xx,
			zz,zz,zz,xx,zz,
			zz,zz,xx,zz,zz,
			zz,xx,zz,zz,zz,
			xx,zz,zz,zz,zz,
			xx,xx,xx,xx,xx};

		static readonly byte[] ssZ = new byte[]
		   {4,
			zz,zz,zz,zz,
			zz,zz,zz,zz,
			xx,xx,xx,xx,
			zz,zz,zz,xx,
			zz,zz,xx,zz,
			zz,xx,zz,zz,
			xx,xx,xx,xx};

		static readonly byte[] s1 = new byte[]
		   {3,
			zz,xx,zz,
			xx,xx,zz,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz,
			xx,xx,xx};

		static readonly byte[] s2 = new byte[]
		   {4,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			zz,zz,zz,xx,
			zz,zz,zz,xx,
			zz,zz,xx,zz,
			zz,xx,zz,zz,
			xx,xx,xx,xx};

		static readonly byte[] s3 = new byte[]
		   {4,
			xx,xx,xx,xx,
			zz,zz,zz,xx,
			zz,zz,zz,xx,
			zz,xx,xx,xx,
			zz,zz,zz,xx,
			zz,zz,zz,xx,
			xx,xx,xx,xx};

		static readonly byte[] s4 = new byte[]
		   {4,
			xx,zz,xx,zz,
			xx,zz,xx,zz,
			xx,zz,xx,zz,
			xx,xx,xx,xx,
			zz,zz,xx,zz,
			zz,zz,xx,zz,
			zz,zz,xx,zz};

		static readonly byte[] s5 = new byte[]
		   {4,
			xx,xx,xx,xx,
			xx,zz,zz,zz,
			xx,zz,zz,zz,
			xx,xx,xx,zz,
			zz,zz,zz,xx,
			zz,zz,zz,xx,
			xx,xx,xx,zz};

		static readonly byte[] s6 = new byte[]
		   {4,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,zz,
			xx,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			zz,xx,xx,zz};

		static readonly byte[] s7 = new byte[]
		   {3,
			xx,xx,xx,
			zz,zz,xx,
			zz,zz,xx,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz,
			zz,xx,zz};

		static readonly byte[] s8 = new byte[]
		   {4,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			zz,xx,xx,zz,
			xx,zz,zz,xx,
			xx,zz,zz,xx,
			zz,xx,xx,zz};

		static readonly byte[] s9 = new byte[]
		   {3,
			xx,xx,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,xx,xx,
			zz,zz,xx,
			zz,zz,xx,
			xx,xx,xx};

		static readonly byte[] s0 = new byte[]
		   {3,
			xx,xx,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,zz,xx,
			xx,xx,xx};

		static readonly byte[] sQuote = new byte[]{3,
					   xx,zz,xx,
					   xx,zz,xx,
					   zz,zz,zz,
					   zz,zz,zz,
					   zz,zz,zz,
					   zz,zz,zz,
					   zz,zz,zz};

		static readonly byte[] sYow = new byte[]{3,
				   zz,xx,zz,
				   xx,xx,xx,
				   xx,xx,xx,
				   xx,xx,xx,
				   zz,xx,zz,
				   zz,zz,zz,
				   zz,xx,zz};

		static readonly byte[] sQuotes = new byte[]{1,
						xx,
						xx,
						zz,
						zz,
						zz,
						zz,
						zz};


		static readonly byte[] sComma = new byte[]{2,
					   zz,zz,
					   zz,zz,
					   zz,zz,
					   zz,zz,
					   zz,zz,
					   zz,xx,
					   xx,zz};

		static readonly byte[] sPeriod = new byte[]{1,
						zz,
						zz,
						zz,
						zz,
						zz,
						zz,
						xx};

		static readonly byte[] sMinus = new byte[]{2,
					   zz,zz,
					   zz,zz,
					   zz,zz,
					   xx,xx,
					   zz,zz,
					   zz,zz,
					   zz,zz};

		static readonly byte[] sQuest = new byte[]{3,
					   xx,xx,xx,
					   zz,zz,xx,
					   zz,zz,xx,
					   zz,zz,xx,
					   zz,xx,xx,
					   zz,zz,zz,
					   zz,xx,zz};

		static readonly byte[] sColon = new byte[]{1,
					   zz,
					   zz,
					   xx,
					   zz,
					   xx,
					   zz,
					   zz};

		static readonly byte[] sch = new byte[]{3,
					zz,xx,zz,
					xx,xx,xx,
					xx,xx,xx,
					xx,xx,xx,
					zz,xx,zz,
					xx,xx,xx,
					xx,xx,xx,};

		static readonly byte[] usc = new byte[]{2,
					zz,zz,
					zz,zz,
					zz,zz,
					zz,zz,
					xx,xx,
					xx,xx,
					xx,xx};

		static readonly byte[] star = new byte[]{5,
					 zz,zz,zz,zz,zz,
					 zz,zz,zz,zz,zz,
					 xx,xx,xx,xx,xx,
					 xx,xx,xx,xx,xx,
					 zz,zz,zz,zz,zz,
					 zz,zz,zz,zz,zz,
					 zz,zz,zz,zz,zz};

		static readonly byte[] ss = new byte[]{2,
				   xx,xx,
				   xx,xx,
				   xx,xx,
				   xx,xx,
				   xx,xx,
				   xx,xx,
				   xx,xx};

		static readonly byte[] sEq = new byte[]{4,
				   zz,zz,zz,zz,
				   xx,xx,xx,xx,
				   zz,zz,zz,zz,
				   zz,zz,zz,zz,
				   xx,xx,xx,xx,
				   zz,zz,zz,zz,
				   zz,zz,zz,zz};

		static readonly byte[] sra = new byte[]{3,
					zz,zz,zz,
					xx,zz,zz,
					xx,xx,zz,
					xx,xx,xx,
					xx,xx,zz,
					xx,zz,zz,
					zz,zz,zz};

		static readonly byte[] slParen = new byte[]{2,
						zz,xx,
						xx,zz,
						xx,zz,
						xx,zz,
						xx,zz,
						xx,zz,
						zz,xx};

		static readonly byte[] srParen = new byte[]{2,
						xx,zz,
						zz,xx,
						zz,xx,
						zz,xx,
						zz,xx,
						zz,xx,
						xx,zz};

		static readonly byte[] ssemic = new byte[]{2,
					   zz,xx,
					   zz,zz,
					   zz,xx,
					   zz,xx,
					   zz,xx,
					   zz,xx,
					   xx,zz};

		static readonly byte[] sSlash = new byte[]{3,
					   zz,zz,zz,
					   zz,zz,xx,
					   zz,zz,xx,
					   zz,xx,zz,
					   zz,xx,zz,
					   xx,zz,zz,
					   xx,zz,zz};

		static readonly byte[] sbSlash = new byte[]{3,
					   zz,zz,zz,
					   xx,zz,zz,
					   xx,zz,zz,
					   zz,xx,zz,
					   zz,xx,zz,
					   zz,zz,xx,
					   zz,zz,xx};

		static readonly byte[] sBlock = new byte[]{3,
						zz,zz,zz,
						zz,zz,zz,
						xx,xx,xx,
						xx,xx,xx,
						xx,xx,xx,
						zz,zz,zz,
						zz,zz,zz};

		static readonly byte[] sBlank = new byte[]{2,
					   zz,zz,
					   zz,zz,
					   zz,zz,
					   zz,zz,
					   zz,zz,
					   zz,zz,
					   zz,zz};

		static readonly byte[] sAT = new byte[]
		{ 5,
		   zz,xx,xx,xx,zz,
		   xx,zz,zz,zz,xx,
		   xx,zz,xx,xx,xx,
		   xx,zz,xx,zz,xx,
		   xx,zz,xx,xx,xx,
		   xx,zz,zz,zz,zz,
		   zz,xx,xx,xx,zz};

		static readonly byte[] sNum = new byte[]
		{ 5,
		  zz,zz,zz,zz,zz,
		  zz,xx,zz,xx,zz,
		  xx,xx,xx,xx,xx,
		  zz,xx,zz,xx,zz,
		  xx,xx,xx,xx,xx,
		  zz,xx,zz,xx,zz,
		  zz,zz,zz,zz,zz};

		static readonly byte[] sBuck = new byte[]
		{5,
			zz,zz,zz,zz,zz,
			zz,zz,zz,zz,zz,
			zz,zz,xx,zz,zz,
			zz,xx,xx,xx,zz,
			xx,xx,xx,xx,xx,
			zz,zz,zz,zz,zz,
			zz,zz,zz,zz,zz};

		static readonly byte[] sPercnt = new byte[]
		{
			5,
			zz,zz,zz,zz,zz,
			xx,zz,zz,zz,xx,
			zz,zz,zz,xx,zz,
			zz,zz,xx,zz,zz,
			zz,xx,zz,zz,zz,
			xx,zz,zz,zz,xx,
			zz,zz,zz,zz,zz};

		static readonly byte[] sCarot = new byte[]
		{ 3,
		  zz,xx,zz,
		  xx,zz,xx,
		  zz,zz,zz,
		  zz,zz,zz,
		  zz,zz,zz,
		  zz,zz,zz,
		  zz,zz,zz};

		static readonly byte[] sCopy = new byte[]
		{ 7,
			zz,xx,xx,xx,xx,xx,zz,
			xx,zz,zz,zz,zz,zz,xx,
			xx,zz,zz,xx,xx,zz,xx,
			xx,zz,xx,zz,zz,zz,xx,
			xx,zz,zz,xx,xx,zz,xx,
			xx,zz,zz,zz,zz,zz,xx,
			zz,xx,xx,xx,xx,xx,zz};

		static readonly byte[] sPtr = new byte[]
		{ 5,
		  xx,zz,zz,zz,zz,
		  xx,xx,zz,zz,zz,
		  xx,xx,xx,zz,zz,
		  xx,xx,xx,xx,zz,
		  xx,xx,xx,xx,xx,
		  zz,zz,xx,zz,zz,
		  zz,zz,zz,xx,zz};

		static readonly byte[] check = new byte[]
		{ 7,
		  xx,zz,zz,zz,zz,zz,xx,
		  zz,xx,zz,zz,zz,xx,zz,
		  zz,zz,xx,zz,xx,zz,zz,
		  zz,zz,zz,xx,zz,zz,zz,
		  zz,zz,xx,zz,xx,zz,zz,
		  zz,xx,zz,zz,zz,xx,zz,
		  xx,zz,zz,zz,zz,zz,xx };

		static readonly byte[] target = new byte[]
		{ 7,
		  zz,zz,zz,xx,zz,zz,zz,
		  zz,zz,xx,zz,xx,zz,zz,
		  zz,xx,zz,zz,zz,xx,zz,
		  xx,zz,zz,xx,zz,zz,xx,
		  zz,xx,zz,zz,zz,xx,zz,
		  zz,zz,xx,zz,xx,zz,zz,
		  zz,zz,zz,xx,zz,zz,zz };

	}

}
