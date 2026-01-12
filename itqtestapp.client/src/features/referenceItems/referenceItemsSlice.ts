import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { fetchReferenceItems as fetchReferenceItemsApi } from '../../services/referenceItemsApi';

export type ReferenceItem = {
  rowNumber?: number;
  code?: number;
  value?: string;
};

export type ReferenceItemsState = {
  items: ReferenceItem[];
  totalCount: number;
  page: number;
  pageSize: number;
  search: string;
  status: 'idle' | 'loading' | 'succeeded' | 'failed';
  error: string | null;
};

type FetchReferenceItemsArgs = {
  page: number;
  pageSize: number;
  search?: string;
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
  search: '',
  status: 'idle',
  error: null,
};

export const fetchReferenceItems = createAsyncThunk<
  FetchReferenceItemsResult,
  FetchReferenceItemsArgs
>('referenceItems/fetch', async ({ page, pageSize, search }) =>
  fetchReferenceItemsApi(page, pageSize, search)
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
    setSearch(state, action: PayloadAction<string>) {
      state.search = action.payload;
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

export const { setPage, setPageSize, setSearch } = referenceItemsSlice.actions;

export default referenceItemsSlice.reducer;
