import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { fetchReferenceItems as fetchReferenceItemsApi } from '../../services/referenceItemsApi';

export type ReferenceItem = {
  rowNumber?: number;
  RowNumber?: number;
  code?: number;
  Code?: number;
  value?: string;
  Value?: string;
};

export type ReferenceItemsState = {
  items: ReferenceItem[];
  totalCount: number;
  page: number;
  pageSize: number;
  status: 'idle' | 'loading' | 'succeeded' | 'failed';
  error: string | null;
};

type FetchReferenceItemsArgs = {
  page: number;
  pageSize: number;
};

type FetchReferenceItemsResult = {
  items: ReferenceItem[];
  totalCount: number;
};

const initialState: ReferenceItemsState = {
  items: [],
  totalCount: 0,
  page: 1,
  pageSize: 10,
  status: 'idle',
  error: null,
};

export const fetchReferenceItems = createAsyncThunk<
  FetchReferenceItemsResult,
  FetchReferenceItemsArgs
>('referenceItems/fetch', async ({ page, pageSize }) =>
  fetchReferenceItemsApi(page, pageSize)
);

const referenceItemsSlice = createSlice({
  name: 'referenceItems',
  initialState,
  reducers: {
    setPage(state, action: PayloadAction<number>) {
      state.page = action.payload;
    },
    setPageSize(state, action: PayloadAction<number>) {
      state.pageSize = action.payload;
      state.page = 1;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchReferenceItems.pending, (state) => {
        state.status = 'loading';
        state.error = null;
      })
      .addCase(fetchReferenceItems.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.items = action.payload.items;
        state.totalCount = action.payload.totalCount;
      })
      .addCase(fetchReferenceItems.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.error?.message ?? 'Unexpected error.';
      });
  },
});

export const { setPage, setPageSize } = referenceItemsSlice.actions;

export default referenceItemsSlice.reducer;
